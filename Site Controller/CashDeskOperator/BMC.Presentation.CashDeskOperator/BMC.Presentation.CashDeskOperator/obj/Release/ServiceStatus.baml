   M S B A M L   `   `   `  ���� S  OBMC.CoreLib, Version=12.5.0.0, Culture=neutral, PublicKeyToken=b2f2c13a504ee1f7X;clr-namespace:BMC.CoreLib.WPF.Controls;assembly=BMC.CoreLibBMC.CoreLib.WPF.Controls   BMC.Presentation.POS$   BMC.Presentation.ServiceStatus   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Ncorlib;clr-namespace:BMC.CoreLib.WPF.Controls;assembly=BMC.CoreLib       � Title$  ServiceStatus�� � WindowStyle$
 None=� � AllowsTransparency .  � SizeToContent$ WidthAndHeight=�   MainScreenBG#���    � WindowStartupLocation$ CenterScreen=� � 	Resources ��%    .��� .+��� $��1��$��Segoe UI)�����    ServiceStatus_xaml_lblStatus#���  $
��Bold"�$��15(�.��� -   $��	gn_status��$��10q� � RowDefinitions �� �� �� .+��� -    ��Name$ lst_ServiceStatus��lst_ServiceStatus��$��0��$
��Auto=�$
��Auto=�$��2,2,2,5q�$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��13(�$��Segoe UI)�	 �� SelectionMode$	 Single=� " BMC_ListView_ListViewItemStyle#���  .   (BMC.CoreLib.WPF.Controls.WpfListViewSort
   Sort
 .    DefaultColumnIndex$ 0�� �� View �� g_ServiceStatus��-   g_ServiceStatus��.P���   ServiceStatus_xaml_Sno#���   �� Width$
 Auto��   PropertyName$	 SNo�� �� CellTemplate �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2�� �� TextAlignment$
 Left=�$��0,0,0,0q����� *SNo+��   ServiceStatus_xaml_Name#���  $
 Auto��$ ServiceName�� �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$
 Left=�$��0,0,0,0q����� *ServiceName+��   ServiceStatus_xaml_Status#���  $	 100��$ ServiceStatus�� �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Center=�$��0,0,0,0q����� *ServiceStatus+���� *	ForeColor+�� -    ��Name$ txt_lastupdated��$��1��$��0��$��5,10,0,5q�$��Segoe UI)�$��14(�$��Italic$�$
��Wrap=�$��Center=��� -   $��btnExit��$��2��$	��100��  
BMC_Button#���  $��Center=�$��116,0,107,3q�$��Bottom=�$��Segoe UI)�$��12(�  ServiceStatus_xaml_btnClose#���  