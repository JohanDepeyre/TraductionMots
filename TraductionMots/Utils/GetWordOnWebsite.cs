using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TraductionMots.Model;

namespace TraductionMots.Utils
{
    public static class GetWordOnWebsite
    {
        /// <summary>
        /// Retourne une liste de mot, pas de doublon et sans apostrophe
        /// </summary>
        /// <returns>Objet de type Game</returns>
        public  static Game GetListWord()
        {
            
            HtmlHolder html = new HtmlHolder();
            html.OpenConnection();

            string innerText = html.GetHtml();

            html.Dispose();

            List<string> words = innerText.Split(' ').ToList();
            List<string> wordsTrier = (from val in words
                                       where !val.Contains("'")
                                     select val).Distinct().ToList();

            List<Word> listeWordsObj = new List<Word>();
            foreach (string word in wordsTrier)
            {
                listeWordsObj.Add(new Word { TextWord=word });
            }

            return new Game { WordsListe=listeWordsObj, WordsCount=listeWordsObj.Count};
        }


    }
}
