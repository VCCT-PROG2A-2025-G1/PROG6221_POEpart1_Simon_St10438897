   M S B A M L   `   `   `  ����C  ?POEpart3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:POEpart3POEpart3  5   	       POEpart3.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:POEpart3     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� .@�5      �� $
��Chat��6   .��5      � $��10q�6    � RowDefinitions 5      �� 5      $��* �6(   �� 5      $
��Auto �6(   .+�5      �� -   5         ��Name$ MessagesList��5      $��0��5      $��Consolas)�5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5      �� 5      $��1��6!    �� Orientation$ 
Horizontal=�6.   $��0,10,0,0q�6G   .+�5      �� -   5         ��Name$ 	UserInput��5   "   $	��600��5      5      �� 5      -   5        $
��Send��5   !   $��75��5      $��10,0,0,0q�5      5       5!      5"      5#      �� 5$      $��Tasks��6   $��20��6%   $	��Top=�61   .��5%      &   System.Windows.Controls.DataGrid  -   5          Name$ TasksDataGrid��5%         AutoGenerateColumns .     CanUserAddRows .  $��10q�6�   ��6e   �� *Tasks+	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Title��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Title+6V     5)      $
 Description��6-   $ 3* 6]    6B   �� *Prompt+6]     5+      $
 Reminder��6-   $ 2* 6\    6?   �� *
Schedule+6\   4  .System.Windows.Controls.DataGridCheckBoxColumn  5-      $
 	Completed��61   $
 Auto 6o    6D   �� *	Completed+ �� Mode$ TwoWay=�6o   4  .System.Windows.Controls.DataGridTemplateColumn  5/      $
 Delete��61   $
 Auto 6A      CellTemplate 50      �� 51   "   .R�52   &   �� -   5        $��Delete��52   -   
 � Tag 6W   �� 53   #   55      57      58      �� 5:      $
��Quiz��6   .��5;      � $��10q�6    5<      �� 5=      $
��Auto �6(   �� 5>      $
��Auto �6(   �� 5?      $
��Auto �6(   �� 5@      $
��Auto �6(   �� 5A      $
��Auto �6(   .+�5D      �� -   5         ��Name$ QuizProgressText��5E      $��0��63   $
��Bold"�6@   $��Question 1 of 10��6R   �� 5G      -   5        $ QuestionText��5H      $��1��6/   $	��0,5q�6<   $
��Wrap=�6I   $�� ��6]   �� 5J      -   5         ��Name$ OptionsPanel��5K      $��2��6/   $	��0,5q�6<   .+�5L      	� -	   5         	�Name$ OptionARadio��5L   &    	� 	GroupName$ QuizOptions��6<   $	��0,2q�6T   	� 5M      -
   5        $ OptionBRadio��5M   &   $ QuizOptions��6<   $	��0,2q�6T   	� 5N      -   5        $ OptionCRadio��5N   &   $ QuizOptions��6<   $	��0,2q�6T   	� 5O      -   5        $ OptionDRadio��5O   &   $ QuizOptions��6<   $	��0,2q�6T   5P      �� 5S      $��3��6!   $ 
Horizontal=�6.   $��0,10,0,0q�6G   .+�5T      �� -   5         ��Name$ SubmitButton��5T   !   $��Submit��67   $��75��6H   6S   �� 5U      -   5        $ 
NextButton��5U   !   $
��Next��65   $��75��6D   $��10,0,0,0q�6O   $y�	Collapsed=�6z   5V      �� 5Y      -   5        $ FeedbackText��5Z      $��4��6/   $��0,10,0,0q�6<   $��Italic$�6N   5[      5\      �� 5^      $��Activity Log��6   .��5_      � $��10q�6   .+�5`        -   5        $ ActivityDataGrid��5`       .   .     
IsReadOnly . ��6~   �� *ActivityLog+	 5a        5c      $

 Time��61   $	 100 6m    6?   �� *	Timestamp+ �� StringFormat$ T��6m     5e      $
 Action��61   $ * 6a    6A   �� *Description+6a   5g      5h      5i      5l      5m      5n      