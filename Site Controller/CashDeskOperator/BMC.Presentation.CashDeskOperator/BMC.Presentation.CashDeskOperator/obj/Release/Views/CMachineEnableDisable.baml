   M S B A M L   `   `   `  ����   BMC.Presentation.POS6    0BMC.Presentation.POS.Views.CMachineEnableDisable   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $	��800��$	��760��  J� RenderTransformOrigin$  0.5,0.5&�.��� $  0.5,0.5&�$	��760��$	��800����Z� .+��� -    ��Name$ 
textHeader��
textHeader��$��
18,7,282,0q�$��30�� �� TextAlignment$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$  0.5,0.5&�   CAftSync_xaml_textHeader#���   ���	#FFFFFFFFF� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_1��$ 	#00C4CFBE��$ 1Q��� Border_3��$��Stretch=�$��Stretch=� �� CornerRadius$ 5,5,5,5�����	#19CDDD98� 	 � RowDefinitions	 �� $	��71* ��� $��435.889* ��� $
��253* �
 � ColumnDefinitions
 �� .+��� -    ��Name$ lstMachineDetails��$��1��$
��Auto=�$
��Auto=�lstMachineDetails��$��7,8,24,0q�$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��13(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���   �� SelectionMode$ Single=�����  �� Mode$ Default=� �� View �� .P��� -   CheckBox��CheckBox��  CAftSync_xaml_chkBox#���   �� CellTemplate �� checkBoxTemplate��.R�� 
Grid_1��$	��100��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��checkMachine��$��Center=�$��Center=� d� 	IsChecked �� *
IsSelected+$ TwoWay=��� -   Machine_Name�� " CAftSync_xaml_textAssetDetails#���   �� 	Name1��.R�� 
Grid_2��$	��160��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� 	Name1��$��16(�$��Center=�$��Center=����� *	StockNo+�� -   
BarPosName��  CAftSync_xaml_BarPosDetails#���   �� 
BarPos��.R�� 
Grid_4��$	��160��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� 
BarPos��$��16(�$��Center=�$��Center=����� *BarPosNumber+�� -   MachineStatus��Status1��  CAftSync_xaml_Status#���   �� Status_1��.R�� 
Grid_3��$	��100��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� MachineStatus��$��16(�$��Center=�$��Center=����� *Status+$ TwoWay=��� -   Message_��Message_��  CAftSync_xaml_Message#���   �� Message_��.R�� 
Grid_3��$	��100��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Message��$��16(�$��Center=�$��Center=����� *	Message+$ TwoWay=�� $��2��$��0,73.75,0,65q�	 �� 
 �� $
��115* ��� $
��147* ��� $
��146* ��� $
��147* ��� $
��130* ��� $��114.999* �.+��� -	   $��	btnEnable��$��1��	btnEnable��  
BMC_Button#���   	 CAftSync_xaml_Button1#��� 	 $��2.001,18,32,36q����� *Enable+����  �� ElementName$ listViewTicketSync���� -
   $��
btnDisable��$��2��
btnDisable��#���   
 CAftsync_xaml_Button2#��� 
 $��
2,18,32,36q����� *	Disable+���� $ listViewTicketSync���� -   $��bttnSelectAll��$��3��bttnSelectAll��#���    CAftSync_xaml_Button3#���  $��1.001,18,37.001,36q����� *	SelectALL+���� $ listViewTicketSync���� -   $��bttnDeSelectAll��$��4��bttnDeSelectAll��#���    CAftSync_xaml_Button4#���  $��0.001,18,18,36q����� *DeSelectALL+���� $ listViewTicketSync��