   M S B A M L   `   `   `  ����D  @CS3280WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CS3280WPF.ItemsCS3280WPF.Items  5   	       CS3280WPF.Items.EditItems   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CS3280WPF.Items     5   	     � Title$  
Edit Items��5   	   $	��600��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65   ��5	      �� 5
   
    �� ImageSource$P J/CS3280WPF;component/Resources/Images/Backgrounds/background_EditItems.jpg��6    j� Stretch$ UniformToFill=�6n   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ itemDataGrid��5      $
��Left=�6)   $	��372��6D   $��	60,29,0,0q�6Q   $	��Top=�6d   $	��665��6|      MinColumnWidth$ 25Q�6�      CanUserSortColumns .     CanUserReorderColumns .  $��Segoe UI Black)�6     SelectionMode$ Single=�6"  
�������	   AutoGenerateColumns	 .  $��18(�6h  
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Code��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 68   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *
ItemCode+  5      $	 410 6%   $ Description��62    6G   �� *
ItemDesc+  5      $	 150 6%   $
 Cost��62    6@   �� *Cost+5      �� 5   
   -   5         ��Name$ itemCodeTextBox��5      $
��Left=�6+   $��32��6F   $��
60,436,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $��	Item Code��6z   $	��Top=�6�   $��92��6�   $��SimSun)�6�    �� 	MaxLength$ 2��6�   $��18(�6�   �� 5   
   -   5        $ itemDescTextBox��5      $��159,436,0,0q�6+   $
 Wrap=�6@   $��Item Description��6T   $
��Left=�6l   $	��274��6�   $��32��6�   $	��Top=�6�   $��SimSun)�6�   $ 25��6�   $��18(�6�   �� 5   
   -   5        $ itemPriceTextBox��5      $
��Left=�6,   $��32��6G   $��438,436,0,0q�6S   $
 Wrap=�6h   $��0.00$��6|   $	��Top=�6�   $��89��6�   $��SimSun)�6�   $ 10��6�   $��18(�6�   �� 5   
   -   5         ��Name$ addItemButton��5      $��Add Item��6(   $
��Left=�6;   $��
60,495,0,0q�6V   $	��Top=�6j   $	��150��6�   $��32��6�   
�������$��SimSun)�6�   $��18(�6�   5      �� 5   
   -   5        $ editItemButton��5      $��	Edit Item��6)   $
��Left=�6=   $��411,495,0,0q�6X   $	��Top=�6m   $	��150��6�   $��32��6�   
�������$��SimSun)�6�   $��18(�6�   5      �� 5   
   -   5        $ removeItemButton��5      $��Remove Item��6+   $
��Left=�6A   $��239,495,0,0q�6\   $	��Top=�6q   $	��151��6�   $��32��6�   
�������$��SimSun)�6�   $��18(�6�   5      �� 5   
   -   5        $ clearSelectionButton��5      $��Clear Selection��6/   $
��Left=�6I   $��575,436,0,0q�6d   $	��Top=�6y   $	��150��6�   $��32��6�   
�������$��SimSun)�6�   $��18(�6�   5      5!      5"      