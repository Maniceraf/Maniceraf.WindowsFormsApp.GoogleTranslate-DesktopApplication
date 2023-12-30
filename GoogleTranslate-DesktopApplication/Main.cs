using Newtonsoft.Json;
using System.Collections;

namespace GoogleTranslate_DesktopApplication
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public string TranslateText(string input)
        {
            try
            {
                string url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", "vi", "en", Uri.EscapeUriString(input));
                HttpClient httpClient = new HttpClient();
                string result = httpClient.GetStringAsync(url).Result;
                var jsonData = JsonConvert.DeserializeObject<List<dynamic>>(result);
                var translationItems = jsonData[0];
                string translation = "";
                foreach (object item in translationItems)
                {
                    IEnumerable translationLineObject = item as IEnumerable;
                    IEnumerator translationLineString = translationLineObject.GetEnumerator();
                    translationLineString.MoveNext();
                    translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
                }
                if (translation.Length > 1) { translation = translation.Substring(1); };
                return translation;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something wrong. Please try again !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ex.Message;
            }
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Text = TranslateText(textBox1.Text);
            }
        }
    }
}
