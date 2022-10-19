using DeepL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraductionMots.Model;

namespace TraductionMots.Utils
{
    
    public static class GetTranslate
    {

        /// <summary>
        /// Retourne la traduction donné par l'api de deepl
        /// </summary>
        /// <param name="wordFr">mot a traduire</param>
        /// <returns>mot traduit</returns>
        public static DeepL.Model.TextResult GetTextResult(string wordFr)
        {
            Translate translate = new Translate();

            DeepL.Model.TextResult traductionDeepl;
            //.result permet d'attendre que la tache soit terminée, on perd l'interet de l'async mais pas possible autrement avec l'api deepl
            traductionDeepl = Task.Run(async () => await translate.OTranslator.TranslateTextAsync(
          wordFr,
          LanguageCode.French,
          LanguageCode.EnglishAmerican)).Result;
            return traductionDeepl;
        }
    }
}
