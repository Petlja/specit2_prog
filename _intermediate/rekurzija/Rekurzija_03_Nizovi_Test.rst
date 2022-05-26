
..
  Рекурзија и низови - тест
  quiz

Рекурзија и низови - тест
=========================


.. fillintheblank:: rekurzija_nizovi_1
		    
   Шта исписује наредни програм (пробај да одговориш без његовог покретања)?

   .. code-block:: csharp

      static int f(int[] a, int n)
      {
          if (n == 0)
	     return 0;
	  return f(a, n-1) + a[n-1]*a[n-1];
      }

      static void Main()
      {
         int[] a = {3, 1, 2};
	 Console.WriteLine(f(a, a.Length));
      }
   

   - :^14$: Тачан одговор!
     :.*: Покушај поново.

.. fillintheblank:: rekurzija_nizovi_2
		    
   Допуни наредну функцију тако да израчунава производ елемената низа. Шта треба да стоји уместо знакова питања?

   .. code-block:: csharp

      static int Proizvod(int[] a, int n)
      {
          if (n == 0)
	     return ???;
	  return Proizvod(a, n-1) * a[n-1];
      }
   

   - :^1$: Тачан одговор!
     :.*: Покушај поново.

	  
.. mchoice:: rekurzija_nizovi_3
   :answer_a: if (a[0] % 2 == 0) return BrojParnih(a, n-1) + 1; else return BrojParnih(a, n-1);
   :answer_b: if (a[n-1] % 2 == 0) return BrojParnih(a, n-1) + 1; else return BrojParnih(a, n-1);
   :answer_c: if (a[n-1] % 2 == 0) return BrojParnih(a, n-1) + a[n-1]; else return BrojParnih(a, n-1);
   :answer_d: if (a[n-1] % 2 != 0) return BrojParnih(a, n-1) + 1; else return BrojParnih(a, n-1);
   :correct: b
   :feedback_a: Не.
   :feedback_b: Тачно!
   :feedback_c: Не.
   :feedback_d: Не.
		
   Шта треба дописати да би функција израчунавала број парних елемената низа?
   
   .. code-block:: csharp

      static int BrojParnih(int[] a, int n)
      {
          if (n == 0)
	     return 0;
	  ???
      }

.. mchoice:: rekurzija_nizovi_4
   :answer_a: i == 0
   :answer_b: i == a.Length
   :answer_c: i < a.Length
   :correct: c
   :feedback_a: Не.
   :feedback_b: Не.
   :feedback_c: Тачно!
		
   Шта треба дописати да би функција у низ ``b`` уписала квадрате елемената низа ``a`` (низови су исте дужине)?
   
   .. code-block:: csharp

      static int IzracunajKvadrate(int[] a, int[] b, int i)
      {
          if (???)
	  {
	      b[i] = a[i] * a[i];
	      IzracunajKvadrate(a, b, i + 1);
	  }
      }

      
.. mchoice:: rekurzija_nizovi_5
   :answer_a: Попуњава листу ``b`` свим речима из листе ``a`` које почињу словом ``A``.
   :answer_b: Попуњава листу ``b`` свим речима из листе ``a`` које почињу великим словом ``A``.
   :answer_c: Попуњава листу ``b`` свим речима из листе ``a`` које почињу великим словом.
   :correct: a
   :feedback_a: Тачно!
   :feedback_b: Не.
   :feedback_c: Не.
		
   Шта ради наредна рекурзивна функција?
   
   .. code-block:: csharp

      static void f(List<string> a, int n, List<string> b)
      {
          if (n == 0)
	     return;
	  f(a, n-1, b);
	  if (Char.ToUpper(a[n-1][0]) == 'A')
	     b.Add(a[n-1]);
      }
