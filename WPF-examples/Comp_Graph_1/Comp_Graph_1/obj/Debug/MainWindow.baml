   M S B A M L   `   `   `  ����G  CComp_Graph_1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Comp_Graph_1Comp_Graph_1  5   	   m iSystem.Windows.Forms.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�mclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization/System.Windows.Forms.DataVisualization.Charting 5   	       Comp_Graph_1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Comp_Graph_1     5   	   {gmclr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization     5   	     � Title$  
MainWindow��5	   	   $
��1000��6   $
��1600��6*   .��5
      � -   5        $��MyGrid��5
      
������  � RowDefinitions 5   
   �� 5      $��9* �6   6*   �� 5      $��* �6   6)   .+�5   
   7  1System.Windows.Forms.Integration.WindowsFormsHost     Child. 5      ;  5System.Windows.Forms.DataVisualization.Charting.Chart  -   5        MyChart��5   �   5      �� 5   
   -   5        $��MyCanvas��5      $��1��6!   60   �� 5   
   -   5        $��1��5      $
��Auto��6   $
��Auto��6,   $��20(�69   $
��Left=�6G   $��Center=�6b   $��500 0q�6}   $��Построить��6�   6�   �� 5   
   $��1��6   $
��Auto��6   $
��Auto��6+   $��20(�68   $
��Left=�6F   $��Center=�6a   $��120 0q�6|   $"��График функции:��6�   6�   �� 5   
   $��1��6   $
��Auto��6   $
��Auto��6+   $��20(�68   $
��Left=�6F   $��Center=�6a   $��300 0q�6|   $
��ρ =��6�   6�   �� 5   
   -   5        $��	MyTextBox��5      $��1��6#   $
��Auto��60   $��30��6>   $��20(�6I   $
��Left=�6W   $��Center=�6r   $��Center=�6�   $��340 0q�6�   6�   �� 5   
   $��1��6   $
��Auto��6   $
��Auto��6+   $��20(�68   $
��Left=�6F   $��Center=�6a   $��370 0q�6|   $��* cos(3 φ)��6�   6�   5      5      