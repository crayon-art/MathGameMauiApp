using MathGameMauiApp.Models;

namespace MathGameMauiApp;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	const int totalQuestions = 5;
	int gamesLeft = totalQuestions;

	public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		CreateNewQuestion();

	}	

	private void CreateNewQuestion()
	{
		var random = new Random();

		firstNumber = GameType != "/" ? random.Next(1,9) : random.Next(1,99);
        secondNumber = GameType != "/" ? random.Next(1, 9) : random.Next(1, 99);

		if(GameType == "/")
		{
			while(firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
				firstNumber = random.Next(1, 99);
				secondNumber = random.Next(1, 99);
			}
		}

		QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber}"; 


    }

	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		int answer;
		if(Int32.TryParse(AnswerEntry.Text, out answer))
		{
            var isCorrect = false;

            switch (GameType)
            {
                case "+":
                    isCorrect = answer == firstNumber + secondNumber;
                    break;

                case "-":
                    isCorrect = answer == firstNumber - secondNumber;
                    break;

                case "x":
                    isCorrect = answer == firstNumber * secondNumber;
                    break;

                case "/":
                    isCorrect = answer == firstNumber / secondNumber;
                    break;
            }

            ProcessAnswer(isCorrect);
            gamesLeft--;
            AnswerEntry.Text = "";

            if (gamesLeft > 0)
            {
                CreateNewQuestion();
            }
            else GameOver();
        }else
		{
			AnswerLabel.Text = $"{AnswerEntry.Text} is not a number. Please enter a valid number.";

        }	
    }

    private void GameOver()
    {
		GameOperation gameOperation = GameType switch
		{
			"+" => GameOperation.Addition,
			"-" => GameOperation.Subtraction,
			"x" => GameOperation.Multiplication,
			"/" => GameOperation.Division,
		};

		QuestionArea.IsVisible = false;
		BackToMenuBtn.IsVisible = true;
        GameOverLabel.Text = $"Game Over! You got {score} out of {totalQuestions} correct";

		App.GameRepo.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score
		});
    }

    private void ProcessAnswer(bool isCorrect)
    {
		if (isCorrect) score+=1;

		AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect!";
    }

	private void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}