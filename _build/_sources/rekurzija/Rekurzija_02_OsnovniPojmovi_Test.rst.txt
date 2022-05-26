
..
  Рекурзија - основни појмови - тест
  quiz

Рекурзија - основни појмови - тест
==================================

.. mchoice:: rekurzija_osnovno_1
   :answer_a: разликује случај нуле и следбеника.
   :answer_b: позива саму себе.
   :answer_c: израчунава факторијел броја
   :correct: b
   :feedback_a: Не.
   :feedback_b: Тачно!
   :feedback_c: Не.
		
   Рекурзивна функција је она која
   
.. mchoice:: rekurzija_osnovno_2
   :answer_a: нуле и следбеника.
   :answer_b: парних и непарних бројева.
   :answer_c: позитивних и негативних бројева.
   :correct: a
   :feedback_a: Тачно!
   :feedback_b: Не.
   :feedback_c: Не.
		
   Када дефинишемо рекурзивну функцију чији је параметар природан
   број, тада обично разликујемо случајеве
   
.. fillintheblank:: rekurzija_osnovno_3
		    
   Која је вредност израза ``f(4)``, ако је функција ``f`` дефинисана на
   следећи начин?

   .. code-block:: csharp

      static int f(int n)
      {
          if (n == 0)
	     return 0;
	  return f(n-1) + n*n*n;
      }
   

   - :^100$: Тачан одговор!
     :.*: Покушај поново.

.. fillintheblank:: rekurzija_osnovno_4
		    
   Која је вредност израза ``f(4)``, ако је функција ``f`` дефинисана на
   следећи начин?

   .. code-block:: csharp

      static int f(int n)
      {
          if (n <= 1)
	     return 0;
	  return f(n-2) + n;
      }
   

   - :^6$: Тачан одговор!
     :.*: Покушај поново.
	  
.. fillintheblank:: rekurzija_osnovno_5
		    
   Која је вредност израза ``s(3, 5)``, ако је функција ``s`` дефинисана на
   следећи начин?

   .. code-block:: csharp

      static int s(int x, int y)
      {
          if (x == 0)
	     return y;
	  return s(x-1, y) + 1;
      }
   

   - :^8$: Тачан одговор!
     :.*: Покушај поново.

.. mchoice:: rekurzija_osnovno_6
   :answer_a: разлику бројева x и y
   :answer_b: производ бројева x и y
   :answer_c: збир бројева x и y
   :correct: b
   :feedback_a: Не.
   :feedback_b: Тачно!
   :feedback_c: Не.
		    
   Шта израчунава функција ``f`` дефинисана на следећи начин?

   .. code-block:: csharp

      static int f(int x, int y)
      {
          if (x == 0)
	     return 0;
	  return f(x-1, y) + y;
      }
   

.. mchoice:: rekurzija_osnovno_7
   :answer_a: f((n-1)*n) + (n-1)
   :answer_b: f((n-1)*n) + (n-1)*n
   :answer_c: f(n-1) + (n-1)*n
   :correct: c
   :feedback_a: Не.
   :feedback_b: Не.
   :feedback_c: Тачно!

   Допуни рекурзивну функцију која израчунава збир :math:`1\cdot 2 +
   2\cdot 3 + \ldots + (n-1)\cdot n`.

   .. code-block:: csharp

      static int f(int n)
      {
          if (n == 0)
	     return 0;
	  return ???;
      }
   
      
Ко жели да зна више?
''''''''''''''''''''

Ево још неколико мало напреднијих питања у вези са рекурзијом.
      
.. fillintheblank:: rekurzija_osnovno_8
		    
   Која је вредност израза ``M(100) + M(99) + M(98) + ... + M(0)``,
   ако је функција ``M`` дефинисана на следећи начин?
   
   .. code-block:: csharp

      static int M(int n)
      {
          if (n <= 100)
	     return M(M(n + 11));
	  return n - 10;
      }

   - :^9191$: Тачан одговор!
     :.*: Покушај поново.
      
.. fillintheblank:: rekurzija_osnovno_9

   Која је вредност израза ``F(2, 3, 2)``,
   ако је функција ``F`` дефинисана на следећи начин?
    		    
   :math:`F(m, n, p) = \left\{
   \begin{array}{ll}
   m + n & \textrm{ako je}\ p = 0\\
   0     & \textrm{ako je}\ p = 1\ \textrm{i}\ n = 0\\
   1     & \textrm{ako je}\ p = 2\ \textrm{i}\ n = 0\\
   m     & \textrm{ako je}\ p > 2\ \textrm{i}\ n = 0\\
   F(m, F(m, n-1, p), p-1) & \textrm{ako je}\ p > 0\ \textrm{i}\ n > 0
   \end{array}\right.`      

   Помоћ: прво одреди шта се израчунава помоћу ``F(m, n, 1)``,
   а затим одреди шта се израчунава помоћу ``F(m, n, 2)``. 
   
   (када решиш задатак, размисли и о томе шта се израчунава помоћу ``F(m, n, 3)``)

   - :^8$: Тачан одговор!
     :.*: Покушај поново.
   
