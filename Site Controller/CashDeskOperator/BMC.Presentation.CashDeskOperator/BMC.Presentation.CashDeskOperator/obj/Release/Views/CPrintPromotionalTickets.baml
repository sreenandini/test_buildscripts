   M S B A M L   `   `   `  ����   BMC.Presentation.POS9    3BMC.Presentation.POS.Views.CPrintPromotionalTickets   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��$	��660��$	��870��$��Center=�.��� -    �Name$ 
LayoutRoot��
LayoutRoot��$	��238��.+��� -    ��Name$ 	txtHeader��	txtHeader�� �� TextAlignment$ Center=�$��40�� &  "CPrintPromotional_xaml_TextBlock_1#���   $
��Wrap=�$��Segoe UI)�$
��Bold"�$��18(�$��
60,12,71,0q�
�������$	��Top=��� -   $��	lblStatus��$��Out Of��$��Center=�
������ $��13(�$��25.377��$	��Top=�$��
12,72,12,0q�$��Segoe UI)��� -   $��lblPrintedTicket��$��Center=�
������ $��13(�$��25.377��$	��Top=�$��Segoe UI)�$��	60,72,0,0q�$
��Left=�$��82���� -   $��lblTotalTicket��$��Center=�
������ $��13(�$��25.377��$	��Top=�$��Segoe UI)�$��Right=�$��92��$��	0,72,47,0q�� -   $��pbPrint��$��12,105.353,12,106.891q� � Value$ 0Q�
������  � LargeChange$ 1Q� � SmallChange$ 1Q�$��1q�$��LeftToRight=� � Triggers :�  :� RoutedEvent$ ProgressBar.Loaded��.[��� .���� .��W� 	 �� 
TargetName$	 pbPrint��
 �� TargetProperty$
 Value� g� Duration$ 0:0:5D��� -   $��	btnCancel��  
BMC_Button#���  $��97��$��	33,0,0,12q� $  CPrintPromotional_xaml_btnCancel#���  $��Segoe UI)�$
��Left=�$��59��$��Bottom=��� -	   $��btnPause��#���   # CPrintPromotional_xaml_btnPause#���  $��Segoe UI)�$��	0,0,23,12q�$��58��$��Right=�$��Bottom=�$��98���� -
    ��Name$ lblErrorMsg��$�� ��$��Center=�
������ $��14(�$��25��$	��Top=�$��1,367,23,-154q�$y�Visible=�$��Segoe UI)�$
��Bold"�
���  ��