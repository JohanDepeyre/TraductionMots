using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepL;

namespace TraductionMots.Model
{
    public class Translate
    {
        public Translator OTranslator { get; set; }
        /// <summary>,
        /// Clé deepl 
        /// </summary>
        const string authKey = "XXXXXXXXXXXXXXX"; // Replace with your key
        public Translate()
        {

            OTranslator = new Translator(authKey);
        }






    }
}
