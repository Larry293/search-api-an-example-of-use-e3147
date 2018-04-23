using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;

namespace RichUsingSearchAPI {
    public partial class Form1 : Form {
        private string searchString = "$match$";
        private string replaceString = "REPLACED";
        private string replaceRtfPart = @"{\rtf1\deff0{\fonttbl{\f0 Times New Roman;}}{\colortbl\red0\green0\blue0 ;\red0\green0\blue255 ;\red255\green0\blue0 ;}{\stylesheet {\ql\cf0 Normal;}{\*\cs1\cf0 Default Paragraph Font;}{\*\cs2\sbasedon1\cf0 Line Number;}}\sectd\pard\plain\ql{\cf2  }{\cf2 (}{\cf2 #$number$}{\cf2 )}\cf2}";

        public Form1() {
            InitializeComponent();

            richEditControl1.LoadDocument(System.IO.Directory.GetCurrentDirectory() + @"\..\..\test.rtf");
        }

        private void button1_Click(object sender, EventArgs e) {
            SearchAndReplace();
        }

        private void SearchAndReplace() {
            int count = 1;
            
            // To prevent flickering
            richEditControl1.Document.BeginUpdate();

            // Get first search result and start iterating
            ISearchResult searchResult = richEditControl1.Document.StartSearch(searchString, SearchOptions.CaseSensitive, SearchDirection.Forward, richEditControl1.Document.Range);

            while (searchResult.FindNext()) {
                searchResult.Replace(String.Empty);
                DocumentRange insertRange = richEditControl1.Document.InsertText(searchResult.CurrentResult.Start, replaceString);
                richEditControl1.Document.InsertRtfText(insertRange.End, replaceRtfPart.Replace("$number$", count.ToString()));
                count++;
            }

            richEditControl1.Document.EndUpdate();
        }
    }
}