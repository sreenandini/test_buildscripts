   M S B A M L   `   `   `  ����   BMC.Presentation.POS&     BMC.Presentation.SiteCheckPoints   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  MyWindow��MyWindow��$	��850��$	��720�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency .    MainScreenBG#���   $
��Left=�$	��Top=� � 	Resources   checkbox d� 	IsChecked  progress �� Orientation � Minimum � Maximum	 � Value��%  &	       &	 X     �� DataTemplate_1��.R��� 
CheckBox_1��{�.  $��0,1,1,1q� �� *Status+�� DataTemplate_2��.R��� StackPanel_1��$ 
Horizontal=�$��Stretch=�.+�� ProgressBar_1��$��Stretch=�$	��280��$ 0Q�$	 100Q�$��20��	 �� *
Progress+.���� -   
 ��Name$

 Root��Root��  LoginBGBrush#���  $��0q�$	��840��$	��720��$
��Left=�$	��Top=�.+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��42��$��2��$	��309�� �� Left$ 14.267��
 �� Top$ 0�� � ColumnDefinitions �� ColumnDefinition_1��$	��19* ��� ColumnDefinition_2��$��372.486* ��� -    ��Name$ lvCheckpoints��lvCheckpoints��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��13(�$��Segoe UI)�  Bmc_ListViewStyleFixedLock#���   " BMC_ListView_ListViewItemStyle#���  $��24.267,67,15.733,144q�$��Disabled=�$	��498��$��803.733������ *LoadListViewCollection+ �� View �� 
GridView_1��.P��� GridViewColumn_1�� ) %SiteCheckPoints_xaml_GridViewColumn_1#���   �� Width$	 200�� �� DisplayMemberBinding �� *Data+�� GridViewColumn_2�� ) %SiteCheckPoints_xaml_GridViewColumn_2#���  $	 200�� �� *StatusDescription+�� pbListViewitem��-   pbListViewitem�� ' #SiteCheckPoints_xaml_pbListViewitem#���  $	 300�� �� CellTemplate# [ �� GridViewColumn_3�� )	 %SiteCheckPoints_xaml_GridViewColumn_3#��� 	 $ 50��# [ � -    �Name$ ProgressBarMain��ProgressBarMain��$��Stretch=�$ 0Q�$	 100Q�$��24��$	��791��$ 24.267��$	 664��F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@ 
 MsgBoxTitleColor#��� 
 $��42��$ 0��$ 0��$	��840���� -    ��Name$ btnExit��btnExit��   SiteCheckPoints_xaml_btnExit#���    BMC_App_Exit#���   J� RenderTransformOrigin$ 0.455,0.272&�$��50��$��42��$	 789��$ 0���� -   $ 	btnCancel��	btnCancel��$��61��$	��100�� " SiteCheckPoints_xaml_btnCancel#���    ButtonStyle1#���  $��12(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand��  
ListBorder#���  $��0��$��Bottom=�$
��Left=�$	 531��$	 587���� -	   $ btnRecoverAgain��btnRecoverAgain�� ( $SiteCheckPoints_xaml_btnRecoverAgain#���  #���  $	 660��$	 587��$
 Hand��$��Segoe UI)�$��12(�$
��Bold"�$��61��#���  $��1��$	��100���� -
    ��Name$ tbHeader��tbHeader��$��
10,10,71,5q�$��NoWrap=����	#FFFFFFFF$��Segoe UI)�$��16(�$y�Visible=�$��2��$��313.335��