   M S B A M L   `   `   `  ����N  JKursyWalut_SebKrupa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:KursyWalut_SebKrupaKursyWalut_SebKrupa  5   	   $    KursyWalut_SebKrupa.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:KursyWalut_SebKrupa     5   	   -   5          �Name$
  okno��5   A    � Title$ Kalkulator Walut - WSB��6	   $	��450��6(   $	��800��65   .��5	      � .+�5
   
   o� $��20��6   $	��Top=�6   .@�5      m� $
��Menu��6   .@�5      m� -   5        $��Ukryj tabelę z kursami��5       m� IsCheckable .  m� 	IsChecked .  6j   m� 5      -   5        $��O programie...��5      63   �� 5      m� 5      -   5        $��Zamknij Program��5      68   5      m� 5      -   5        $"��Pobierz aktualne kursy walut��5      6=   5      � 5   
   $
��0,20q�6    � ColumnDefinitions 5      �� 5      $��2* �6#   �� 5      $��3* �6#   .+�5      �� -   5         ��Name$ spPrzelicznik��5      $��Center=�60   $��20q�6M   $��0��6Y   $��Center=�6i   .+�5      �� $��Center=�6   .��65   
Przelicz z6A   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ 
txtWaluta1��5      $	��150��62   $��30��6>   $��10q�6J   6V   �� 5      -   5         ��Name$ 	cbWaluta1��5      $��80��62   $��10q�6=   6I   5      �� 5      $��Center=�6   .��65   Przelicz na6B   �� 5      $ 
Horizontal=�6   .+�5       �� -	   5        $ 
txtWaluta2��5       $	��150��62   $��30��6>   $��10q�6J   {�.  6{   �� 5!      -
   5        $ 	cbWaluta2��5!      $��80��62   $��10q�6=   6I   5"      5$      �� 5%      -   5        $ spKursy��5%      $��1��6*   $��Center=�6:   $��10q�6W   .+�5&      �� -   5        	 ��Name$	 numerTabeli��5&      $��Center=�6-   �� 5'      -   5        $	 dataNotowania��5'      $��Center=�6/   �� 5(      -   5        $	 dataPublikacji��5(      $��Center=�60   &   System.Windows.Controls.DataGrid  5)      -   5        
  Name$
 dgKursy��5)      $	��350��6,      AutoGenerateColumns .     
IsReadOnly .    Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ Waluta��6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *nazwa_waluty+6N     5,      $ Symbol��6L    6-   �� *
kod_waluty+6L     5-      $ Przelicznik��6M    6-   �� *przelicznik+6M     5.      $ Średni Kurs��6M    6-   �� *kurs_sredni+6M     5/      $ 
Kurs Kupna��6L    6-   �� *
kurs_kupna+6L     50      $ Kurs Sprzedaży��6P    6-   �� *kurs_sprzedazy+6P   52      53      54      �� 55   
   $��wsb.png��6   $
��Left=�6!   $��Bottom=�6<   $��50��6W   $��10,25q�6b   �� 56   
   $
��Left=�6   $��Bottom=�6+   .��6F   +)Sebastian Krupa, Marcin Kriger | 2019 WSB6q   57      58      