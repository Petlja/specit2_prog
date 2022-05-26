
..
  Динамичко програмирање - новчићи
  reading

Кусур са најмањим бројем новчића
================================

.. questionnote::

   Ако је познат низ новчаних апоена које имамо на располагању, напиши
   програм који одређује најмањи број новчића да би се исплатио дати
   износ (претпоставити да су апоени и износ природни бројеви).

У наредном видео-снимку можеш видети детаљно објашњење неколико решења
овог проблема.
   
.. ytpopup:: 7g5BhFU0sFo
      :width: 735
      :height: 415
      :align: center

Издвојимо наредна два решења динамичким програмирањем навише.

Прво је засновано на идеји да се за сваки износ анализирају све могућности
за последњи исплаћени новчић током враћања кусура.

.. code-block:: csharp

   static int NajmanjiBrojNovcica(int[] novcici, int kusur)
   {
       int[] dp = new int[kusur + 1];
       dp[0] = 0;
       for (int k = 1; k <= kusur; k++)
       {
           int beskonacno = int.MaxValue;
           int min = beskonacno;
           foreach (int novcic in novcici)
               if (novcic <= k)
                  min = Math.Min(min, dp[k - novcic]);
           dp[k] = min == beskonacno ? beskonacno : min + 1;
       }
       return dp[kusur];
   }


На сајту Математичког факултета Универзитета у Београду можеш
испробати `апликацију
<http://www.matf.bg.ac.rs/~filip/algoritmi/dp/novcici.html>`__ која ће ти
помоћи да провериш колико разумеш како овај алгоритам функционише.

Друго је засновано на идеји да се за сваки новчани апоен анализира могућност
да буде или да не буде узиман током враћања кусура.
   
.. code-block:: csharp
   
   static int NajmanjiBrojNovcica(int[] novcici, int kusur)
   {
       const int beskonacno = int.MaxValue;
       int[] dp = new int[kusur + 1];
       dp[0] = 0;
       for (int k = 1; k <= kusur; k++)
          dp[k] = beskonacno;
       for (int n = 1; n <= novcici.Length; n++)
           for (int k = novcici[n-1]; k <= kusur; k++)
              if (dp[k - novcici[n-1]] != beskonacno)
                 dp[k] = Math.Min(dp[k], dp[k-novcici[n-1]] + 1);
       return dp[kusur];
   }
   
На крају извршавања овог алгоритма добија се потпуно идентичан низ као
и у случају извршавања претходног, једино што се низ попуњава другим
редоследом. Стога за контролу можеш испробати исту `апликацију
<http://www.matf.bg.ac.rs/~filip/algoritmi/dp/novcici.html>`__
