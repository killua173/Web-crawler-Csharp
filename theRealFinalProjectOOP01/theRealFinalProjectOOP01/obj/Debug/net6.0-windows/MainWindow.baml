   M S B A M L   `   `   `  ����S  OtheRealFinalProjectOOP01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:theRealFinalProjectOOP01theRealFinalProjectOOP01  5   	   )    #theRealFinalProjectOOP01.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:theRealFinalProjectOOP01     5   	   -   5          � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� -   5         ��Name$ txtcrawlingsite��5      $��Enter site here��6/    �� TextWrapping$
 Wrap=�6F   $	��420��6Z   5   :   �� 5      -   5         ��Name$ btnAdd��5      $	��Add��6%   $��75��63    J� RenderTransformOrigin$ 0.5,0.5&�6S   z�5      V� 5       .~�5!      �� �� 5"      �� 5#       �� Angle$ -0.292Q�6*   T� 5$      5%      5'      �� 5(      $��Threads per site:��6   $��98��60   �� 5)      -   5        $ txtNumberOFthreads��5)      $��10��62   $
 Wrap=�6<   $��24��6P   $��16(�6[   $��21��6i   5*      �� 5+   
   $ 
Horizontal=�6   $��1��6.   .+�5,      �� -   5        $ btnStart��5,      $��Start��6'   $��75��67   6B   �� 5-      -   5        $ btnPauseAndResum��5-      $��Pause��6/   $��75��6?   6J   �� 5.      -   5         ��Name$ CbinternalURLSOnly��5.      $��Crawl external URLs��63   $	��200��6Q   5/   <   50      �� 51   
   -   5        $ btnExit��51      $��X��6"   $��30��6.   $��25��69   $��0��6E   $��Right=�6R   ���	#FFE12626$
��Bold"�52   (   $ 1.237,-0.483&�6:   $��0,0,10,0q�6_   $	��Top=�53      6)   �� 55   
   -	   5        $ 	btnLatest��55      $��URLs & Errors��6$   $��286,56,404,-58q�6X   $��12��6p   �� 57   
   -
   5        	 ��Name$	 label1��57      $�� ��6    $��2��6+   �� 58   
   -   5        $	 label2��58      $�� ��6    $��3��6+   �� 59   
   -   5        $	 label3��59      $�� ��6    $��4��6+   �� 5:   
   -   5        $	 label4��5:      $�� ��6    $��5��6+   �� 5;   
   -   5        $	 label5��5;      $�� ��6    $��6��6+   �� 5<   
   -   5        $	 label6��5<      $�� ��6    $��7��6+   �� 5=   
   -   5        $	 label7��5=      $�� ��6    $��8��6+   �� 5>   
   -   5        $	 label8��5>      $�� ��6    $��9��6+   �� 5?   
   -   5        $	 label9��5?      $�� ��6    $��10��6+   �� 5@   
   -   5        $	 label10��5@      $�� ��6!   $��11��6,   �� 5A   
   -   5        $	 lbTotalTasks��5A      $�� ��6&   $��12��61   �� 5B   
   -   5        $ 
btnAllURLS��5B      $��AllURLS��6%   $��400,56,287,-58q�67   $��12��6O   6]   5C      5D      