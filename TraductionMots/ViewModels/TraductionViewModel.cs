
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TraductionMots.Model;
using TraductionMots.Utils;


namespace TraductionMots.ViewModels;

public class TraductionViewModel : INotifyPropertyChanged
{
    #region Private
    private bool _isVisibleElement;
    private bool _isVisibleSuivant;
    private bool _isVisibleValider;
    private bool _isVisibleDebuter = true;
    private string _wordFr;
    private string _answerWord;
    private DeepL.Model.TextResult _traductionDeepl;
    private int _indexQuestion;
    private Color _answerColor;
    private string _okOrNotOk;
    private int scoreInt = 0;
    private string infoScore;
    #endregion

    #region Getters/Setters
    public event PropertyChangedEventHandler PropertyChanged;
    public Color AnswerColor {get => _answerColor;  set { _answerColor = value; OnPropertyChanged(); }  }
    public Label MotLabel { get; set; }

    public string WordFr {get => _wordFr; set { _wordFr = value;OnPropertyChanged();  }   }
    public string OkOrNotOk{get => _okOrNotOk;set { _okOrNotOk = value; OnPropertyChanged();  }  }
    public string AnswerWord {get {return _answerWord; } set { _answerWord = value;OnPropertyChanged(); }  }
    public Game GameTrad { get; set; }
    public bool IsVisibleElement  {get => _isVisibleElement;set {_isVisibleElement = value; OnPropertyChanged();}    }
    public bool IsVisibleSuivant { get => _isVisibleSuivant; set { _isVisibleSuivant = value; OnPropertyChanged(); } }
    public bool IsVisibleValider { get => _isVisibleValider; set { _isVisibleValider = value; OnPropertyChanged(); } }
    public int ScoreInt { get => scoreInt; set { scoreInt = value; OnPropertyChanged(); } }
    public string InfoScore { get => infoScore; set { infoScore = value; OnPropertyChanged(); } }

    [DefaultValue(true)]
    public bool IsVisibleDebuter { get => _isVisibleDebuter; set { _isVisibleDebuter = value; OnPropertyChanged(); }  }
    #endregion

    #region Command
    public Command BeginGameCommand { get; set; }
    public Command NextQuestionCommand { get; set; }
    public Command ValidAnswerCommand { get; set; }
 
    #endregion


    #region Constructeur
    public TraductionViewModel()
    {
        BeginGameCommand = new Command(BeginGame);
        ValidAnswerCommand = new Command(ValidAnswerSync);
        NextQuestionCommand = new Command(NextQuestion);
    }
    #endregion
    #region Methodes publiques
    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Debut du jeu
    /// </summary>
    public void BeginGame()
    {

        ChangeVisibleElementStart();
        GameTrad = GetWordOnWebsite.GetListWord();
        
        ReturnWordFr(0, GameTrad);
        VisibleValiderOuSuivant("valider");
        ChangeVisibleBtnDebuter(false);

    }

    /// <summary>
    /// Verifie  la reponse du mot traduit par deepl avec le mot saisi 
    /// </summary>
    public void ValidAnswerSync()
    {

        _traductionDeepl = GetTranslate.GetTextResult(WordFr);
        string reponse = AnswerWord ?? "";
        if (reponse.ToLower() == _traductionDeepl.ToString().ToLower())
        {
            ChangeAppearance(true);
            OkOrNotOk = "Bonne réponse";
            ScoreInt += 1;
        }
        else
        {
            ChangeAppearance(false);
            OkOrNotOk = "Mauvaise réponse. La bonne réponse est " + _traductionDeepl;


        }
        
        VisibleValiderOuSuivant("suivant");
        FillScoreLabel();
    }
    /// <summary>
    /// fait les opérations pour afficher le mot suivant
    /// </summary>
    public void NextQuestion()
    {

        ReturnWordFr(IncrementIndex(), GameTrad);
        CleanAnswer();
        VisibleValiderOuSuivant("valider");
    }
    #endregion

    #region Methode Private
    /// <summary>
    /// Fill le label qui affiche le score (exemple : Score : 3/6 )
    /// </summary>
    private void FillScoreLabel()
    {
        if (GameTrad.WordsCount == _indexQuestion + 1)
        {
            EndGame();
            ChangeVisibleBtnDebuter(true);
            VisibleFinDuJeu();
        }
        else
        {
            ScoreLive();
        }

    }
    /// <summary>
    /// Message du score de fin du jeu
    /// </summary>
    private void EndGame()
    {
        InfoScore = "Score final: " + ScoreInt.ToString() + "/" + GameTrad.WordsCount.ToString();
      
    }
    /// <summary>
    /// message du score pour le jeu pas encore terminé (en cours)
    /// </summary>
    private void ScoreLive()
    {

        InfoScore = "Score : " + ScoreInt.ToString() + "/" + (_indexQuestion + 1).ToString() ;
    }
    /// <summary>
    /// Change la couleur de la police en fonction de la réponse
    /// </summary>
    private void ChangeAppearance(bool VraiOuFaux)
    {
        if (VraiOuFaux)
        {
            AnswerColor = Color.FromArgb("#0D803E");
        }
        else
        {
            AnswerColor = Color.FromArgb("#A45A52");
        }
       
        
        
    }
    /// <summary>
    /// Propriété du contenu du label Answer remis à vide
    /// </summary>
    private void CleanAnswer()
    {

        AnswerWord = "";
    }
    /// <summary>
    ///  Donne à l'attribut WordFr la valeur à l'indice de la liste tant que celui ci est inferieur au nombre de mots
    /// </summary>
    /// <param name="index">index de l'item a recupérer</param>
    /// <param name="game">objet de type game contenant un attribut avec une liste de mots</param>
    private void ReturnWordFr(int index, Game game)
    {
        if (index < game.WordsCount)
        {
            WordFr = game.WordsListe[index].TextWord;

        }
        

    }
 
    private void ChangeVisibleElementStart()
    {
        IsVisibleElement = true;
    }

    /// <summary>
    /// gestion de la visibilité du bouton Suivant et Valider ( jamais les deux visibles en mémes temps) s
    /// </summary>
    /// <param name="validerOrSuivant"></param>
    private void VisibleValiderOuSuivant(string validerOrSuivant)
    {
        if (validerOrSuivant == "valider")
        {
            IsVisibleValider = true;
            IsVisibleSuivant = false;
        }
        else if (validerOrSuivant == "suivant") 
        {
            IsVisibleValider = false;
            IsVisibleSuivant = true;
        }
        
    }
    /// <summary>
    /// Quand derniére questionn , les boutons valider et Suivant sont masqué
    /// </summary>
    private void VisibleFinDuJeu()
    {
     
            IsVisibleValider = false;
            IsVisibleSuivant = false;
      

    }
    private void ChangeVisibleBtnDebuter(bool visible)
    {

        IsVisibleDebuter = visible;
    }
    /// <summary>
    /// Incremente l'index de la question de +1
    /// </summary>
    /// <returns></returns>
    private int IncrementIndex()
    {

       return _indexQuestion += 1;

    }
    #endregion
}
