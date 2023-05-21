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
			//Console.WriteLine("\n\nФасад (Facade)\n");
			TextEditor textEditor = new TextEditor();
			Compiller compiller = new Compiller();
			CLR clr = new CLR();

			VisualStudioFacade ide = new VisualStudioFacade(textEditor, compiller, clr);

			Programmer programmer = new Programmer();

			IFigure figure = new Rectangle(30, 40);
			IFigure clonedFigure = figure.Clone();
			figure.GetInfo();
			clonedFigure.GetInfo();

			//Фасад (Facade)_________________________________________

			//Хранитель (Memento)____________________________________
			//Console.WriteLine("\n\nХранитель (Memento)\n");
			Application_ Appl = new Application_();
			Appl.Programming(); // делаем выстрел, осталось 9 строк
			Application_History App_hist = new Application_History();



			Appl.Programming(); //пишем код, осталось 8 строк
			Appl.Programming();
			Appl.Programming();
			App_hist.History.Push(Appl.SaveState()); // сохраняем код
			Appl.RestoreState(App_hist.History.Pop());

			programmer.CreateApplication(ide);
			//Appl.Programming(); //пишем код, осталось 8 строк


			//Хранитель (Memento)____________________________________

			//Прототип (Prototype)____________________________________
			//Console.WriteLine("\n\nПрототип (Prototype)\n");




			Console.Read();
			//Прототипototype)____________________________________
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
	class Application_
	{
		private int lines = 10; // кол-во строк
		private int hours = 1; // кол-во жизней

		public void Programming()
		{
			if (lines % 2 == 0)
			{
				hours++;
			}
			if (lines > 0)
			{
				lines--;
				Console.WriteLine("Пишем код. Осталось {0} строк", lines);
			}
			else
				Console.WriteLine("Программа написана");
		}
		// сохранение состояния
		public AppMemento SaveState()
		{
			Console.WriteLine("Сохранение программы. Параметры: {0} строк, {1} часов", lines, hours);
			return new AppMemento(lines, hours);
		}

		// восстановление состояния
		public void RestoreState(AppMemento memento)
		{
			this.lines = memento.Lines;
			this.hours = memento.Hours;
			Console.WriteLine("Восстановление программы. Параметры: {0} строк, {1} часов", lines, hours);
		}
	}
	// Memento
	class AppMemento
	{
		public int Lines { get; private set; }
		public int Hours { get; private set; }

		public AppMemento(int lines, int hours)
		{
			this.Lines = lines;
			this.Hours = hours;
		}
	}

	// Caretaker
	class Application_History
	{
		public Stack<AppMemento> History { get; private set; }
		public Application_History()
		{
			History = new Stack<AppMemento>();
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
			Console.WriteLine("Форма длиной {0} и шириной {1}", height, width);
		}
	}


	//Прототип (Prototype)____________________________________
}
