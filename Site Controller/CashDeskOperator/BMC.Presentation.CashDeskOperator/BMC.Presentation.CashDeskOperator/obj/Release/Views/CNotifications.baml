   M S B A M L   `   `   `  ����   BMC.Presentation.POS/    )BMC.Presentation.POS.Views.CNotifications   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :Custom-http://schemas.microsoft.com/wpf/2008/toolkit  -     � Title$  Notifications��$	��533��$
��1024�� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency .  � WindowStyle$
 None=�   MainScreenBG#���   .��� -   $��	gMainGrid��$��	0,0,10,10q�$��Right=�$
��1007�� � ColumnDefinitions �� $��33 ��� $	��606 ��� $	��124 ��� $
��Auto �$	��125���� $
��Auto �$	��144�� � RowDefinitions �� $��59 ��� $��40 ��� $	��353 ��� $��71 �.+��� -   $��lbl_Notifications��$��0,20,0,0q�$��Notifications��$��1��$��0��$
��Left=�$��Center=�$	��456��
�������$��18(�$��39��$
��Bold"��� -    ��Name$ btnExit��$��0��$��4��$��Right=�btnExit��$��45��$��45��  Login_xaml_btnExit#���    BMC_App_Exit#���  $	��Top=�$��0,4,35,0q��� -   $��cb_SelectAll��$��
10,5,556,0q�$��1��$��Segoe UI)�$��16(�$
��Bold"�$��1��$��36��$	��Top=�$��2���� -   $��lbl_SelectAll��$��68,5,0,7q�$��
Select All��$��1��$��1��$
��Left=�$��Center=�$��96��
�������$��15(�$��28���� -   $��
lbl_Status��$��1��$��3��
�������$��15(�$��10,3,163,-3q��� -   $��btn_Refresh��$��2��$��
0,10,6,-21q�$��3��  
BMC_Button#���  $��Refresh��$��Right=�$	��100���� -	   $��	btn_Clear��$��3��$��18,10,0,-21q�$��3��#���  $��Clear��$
��Left=�$	��100���� -
   $��	btn_Close��$��4��$��0,10,35,-21q�$��3��#���  $��Close��$��Right=�$	��100��)  #Microsoft.Windows.Controls.DataGrid  -     Name$ dgNotificationsDetails��dgNotifications��   SelectionUnit$ FullRow=�	   
IsReadOnly	 . $��13(�$��Segoe UI)�$��0,0,34,0q�
   AutoGenerateColumns
 .    dgstyle#���  
�������$��5q� 	 RowBG   RowBackground# �     HorizontalGridLinesBrush �	#FF6B747A   VerticalGridLinesBrush �	#FF6B747A"   VerticalScrollBarVisibility$
 Auto=�$   HorizontalScrollBarVisibility$
 Auto=�   ClipboardCopyMode$ IncludeHeader=�$��2��$��4��$��Right=�$	��955��$��1��$	��351��$	��Top=�   SelectionMode$ Extended=�����    Columns 3  -Microsoft.Windows.Controls.DataGridTextColumn  /  )Microsoft.Windows.Controls.DataGridColumn   MinWidth$	 160Q�   Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$
 Auto    Header$
 Type��4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding �� *NotificationItem+  $	 615Q�$
 Auto $ Notification�� �� *Notifications+  $	 160Q�$
 Auto $ Notified Time�� �� *NotifiedDate+