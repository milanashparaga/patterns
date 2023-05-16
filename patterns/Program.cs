using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace patterns
{



	internal class Program
	{
		static void Main(string[] args)
		{
			//Фасад (Facade)_________________________________________
			Console.WriteLine("\n\nФасад (Facade)\n");
			TextEditor textEditor = new TextEditor();
			Compiller compiller = new Compiller();
			CLR clr = new CLR();

			VisualStudioFacade ide = new VisualStudioFacade(textEditor, compiller, clr);

			Programmer programmer = new Programmer();
			programmer.CreateApplication(ide);

			
			//Фасад (Facade)_________________________________________

			//Хранитель (Memento)____________________________________
			Console.WriteLine("\n\nХранитель (Memento)\n");
			Hero hero = new Hero();
			hero.Shoot(); // делаем выстрел, осталось 9 патронов
			GameHistory game = new GameHistory();

			game.History.Push(hero.SaveState()); // сохраняем игру

			hero.Shoot(); //делаем выстрел, осталось 8 патронов

			hero.RestoreState(game.History.Pop());

			hero.Shoot(); //делаем выстрел, осталось 8 патронов

		
			//Хранитель (Memento)____________________________________

			//Прототип (Prototype)____________________________________
			Console.WriteLine("\n\nПрототип (Prototype)\n");
			IFigure figure = new Rectangle(30, 40);
			IFigure clonedFigure = figure.Clone();
			figure.GetInfo();
			clonedFigure.GetInfo();

			figure = new Circle(30);
			clonedFigure = figure.Clone();
			figure.GetInfo();
			clonedFigure.GetInfo();

			Console.Read();
			//Прототип (Prototype)____________________________________
		}

	}

	//Фасад (Facade)_________________________________________
	// текстовый редактор
	class TextEditor
	{
		public void CreateCode()
		{
			Console.WriteLine("Написание кода");
		}
		public void Save()
		{
			Console.WriteLine("Сохранение кода");
		}
	}
	class Compiller
	{
		public void Compile()
		{
			Console.WriteLine("Компиляция приложения");
		}
	}
	class CLR
	{
		public void Execute()
		{
			Console.WriteLine("Выполнение приложения");
		}
		public void Finish()
		{
			Console.WriteLine("Завершение работы приложения");
		}
	}

	class VisualStudioFacade
	{
		TextEditor textEditor;
		Compiller compiller;
		CLR clr;
		public VisualStudioFacade(TextEditor te, Compiller compil, CLR clr)
		{
			this.textEditor = te;
			this.compiller = compil;
			this.clr = clr;
		}
		public void Start()
		{
			textEditor.CreateCode();
			textEditor.Save();
			compiller.Compile();
			clr.Execute();
		}
		public void Stop()
		{
			clr.Finish();
		}
	}

	class Programmer
	{
		public void CreateApplication(VisualStudioFacade facade)
		{
			facade.Start();
			facade.Stop();
		}
	}
	//Фасад (Facade)_________________________________________


	//Хранитель (Memento)____________________________________
	// Originator
	class Hero
	{
		private int patrons = 10; // кол-во патронов
		private int lives = 5; // кол-во жизней

		public void Shoot()
		{
			if (patrons > 0)
			{
				patrons--;
				Console.WriteLine("Производим выстрел. Осталось {0} патронов", patrons);
			}
			else
				Console.WriteLine("Патронов больше нет");
		}
		// сохранение состояния
		public HeroMemento SaveState()
		{
			Console.WriteLine("Сохранение игры. Параметры: {0} патронов, {1} жизней", patrons, lives);
			return new HeroMemento(patrons, lives);
		}

		// восстановление состояния
		public void RestoreState(HeroMemento memento)
		{
			this.patrons = memento.Patrons;
			this.lives = memento.Lives;
			Console.WriteLine("Восстановление игры. Параметры: {0} патронов, {1} жизней", patrons, lives);
		}
	}
	// Memento
	class HeroMemento
	{
		public int Patrons { get; private set; }
		public int Lives { get; private set; }

		public HeroMemento(int patrons, int lives)
		{
			this.Patrons = patrons;
			this.Lives = lives;
		}
	}

	// Caretaker
	class GameHistory
	{
		public Stack<HeroMemento> History { get; private set; }
		public GameHistory()
		{
			History = new Stack<HeroMemento>();
		}
	}
	//Хранитель (Memento)____________________________________

	//Прототип (Prototype)____________________________________
	interface IFigure
	{
		IFigure Clone();
		void GetInfo();
	}

	class Rectangle : IFigure
	{
		int width;
		int height;
		public Rectangle(int w, int h)
		{
			width = w;
			height = h;
		}

		public IFigure Clone()
		{
			return new Rectangle(this.width, this.height);
		}
		public void GetInfo()
		{
			Console.WriteLine("Прямоугольник длиной {0} и шириной {1}", height, width);
		}
	}

	class Circle : IFigure
	{
		int radius;
		public Circle(int r)
		{
			radius = r;
		}

		public IFigure Clone()
		{
			return new Circle(this.radius);
		}
		public void GetInfo()
		{
			Console.WriteLine("Круг радиусом {0}", radius);
		}
	}
	//Прототип (Prototype)____________________________________
}
