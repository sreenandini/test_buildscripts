   M S B A M L   `   `   `  ����   BMC.Presentation.POS(    "BMC.Presentation.POS.Views.CUnlock   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :Custom-http://schemas.microsoft.com/wpf/2008/toolkit  P LAvalonControlsLibrary, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullJCustom1<http://schemas.AvalonControls/AvalonControlsLibrary/Controls  -     >�Name$  UserControl��$��Stretch=�$��Stretch=� J� RenderTransformOrigin$ 0.5,0.5&�.��� $��Stretch=�$��Stretch=�$��15,10,15,10q�$ 0.5,0.5&���Z� .+��� -    ��Name$ 	txtHeader��$��0��	txtHeader��$��18,20,350,0q� �� TextAlignment$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$
��Left=�$ 0.5,0.5&�   CUnlock_xaml_txtHeader#���   ���	#FFFFFFFFF� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� Border_3��$��Stretch=�$��Stretch=�	 �� CornerRadius$	 5,5,5,5�����	#19CDDD98� 
 � RowDefinitions
 �� $��70 ��� $��55 ��� $	��400 ��� $��55 � � ColumnDefinitions �� �� �� �� �� .+��� -   $��
lblAppType��$��13(�$��Segoe UI)�
�������$��Center=�$��Right=�$��1��$��0��
lblAppType��$��5,0,0,10q�  CUnlock_xaml_lblAppType#���  �� -   $��
cmbLockApp��$��1��$��1�� �� DisplayMemberPath$ HS_Type��  BMC_ComboBoxStyle#���  $��13(�$��30���� -   $��
lbLockType��$��13(�$��Segoe UI)�
�������$��Center=�$��Right=�$��1��$��2��
lbLockType��$��5,0,0,10q�  CUnlock_xaml_lbLockType#���  �� -   $��cmbLockType��$��3��$��1��#���  $��13(�$��30���� -   $��btn_GetMessages��$��4��$��1��$��Center=�btn_GetMessages��  
BMC_Button#���     CUnlock_xaml_btn_GetMessages#���  $	��137��$��55��$	��Top=��� -    ��Name$ 
lvLockData��$��2��$��5��$
��Auto=�$
��Auto=�
lvLockData��$��	7,8,24,13q�$��Stretch=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��13(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���   �� SelectionMode$ Single=�����  �� Mode$ Default=� �� View �� -	   gvUnlock��.P��� -
   clmnLock_ID�� �� Width$ 0��  CUnlock_xaml_clmnLock_ID#���   �� CellTemplate �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Lock_ID������ *	Lock_ID+�� -   clmnLock_User�� 	 CUnlock_xaml_clmnLock_User#��� 	  �� T9��.R�� G0��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� 	Lock_User������ *	Lock_User+�� -   clmnLock_Machine�� !
 CUnlock_xaml_clmnLock_Machine#��� 
  �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Lock_Machine������ *Lock_Machine+�� -   clmnLock_Application�� % !CUnlock_xaml_clmnLock_Application#���   �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Lock_Application������ *Lock_Application+�� -   clmnLock_Type��  CUnlock_xaml_clmnLock_Type#���   �� T9��.R�� G0��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� 	Lock_Type��$��75������ *	Lock_Type+�� -   clmnLock_Created�� ! CUnlock_xaml_clmnLock_Created#���   �� T9��.R�� G0��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Lock_Created��$	��157������ *Lock_Created+�� -   clmnisSelected��$�� �� �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� chkIsSelected�� d� 	IsChecked �� *
IsSelected+$ TwoWay=��� -   $��btn_SelectAll��$��2��$��3��$��Center=�btn_SelectAll��#���    CUnlock_xaml_btn_SelectAll#���  $	��137��$��55��$	��Top=��� -   $��btn_DeSelectAll��$��3��$��3��$��Center=�btn_DeSelectAll��#���     CUnlock_xaml_btn_UnSelectAll#���  $	��137��$��55��$	��Top=��� -   $��
btn_Unlock��$��4��$��3��$��Center=�btn_GetMessages��#���    CUnlock_xaml_btn_Unlock#���  $	��137��$��55��$	��Top=�