   M S B A M L   `   `   `  ����   BMC.Presentation.POS/    )BMC.Presentation.POS.Views.CExportDetails   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :Custom-http://schemas.microsoft.com/wpf/2008/toolkit  P LAvalonControlsLibrary, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullJCustom1<http://schemas.AvalonControls/AvalonControlsLibrary/Controls  -     >�Name$  UserControl�� J� RenderTransformOrigin$ 0.5,0.5&�$��Stretch=�$��Stretch=�.��� $��Stretch=�$��Stretch=�$��15,10,15,10q�$ 0.5,0.5&���Z� .+��� -    ��Name$ 	txtHeader��$��0��	txtHeader��$��18,20,350,0q� �� TextAlignment$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$
��Left=�$ 0.5,0.5&� !  CExportDetails_xaml_txtHeader#���   ���	#FFFFFFFFF� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� Border_3��$��Stretch=�$��Stretch=�	 �� CornerRadius$	 5,5,5,5�����	#19CDDD98� 
 � RowDefinitions
 �� $��55 ��� $	��450 ��� $��55 ��� $��40 ��� $��40 � � ColumnDefinitions �� �� �� �� �� .+��� -    ��Name$ lvUnExportData��$��1��$��5��$
��Auto=�$
��Auto=�lvUnExportData��$��	7,8,24,13q�$��Stretch=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��13(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���   �� SelectionMode$ Single=�����  �� Mode$ Default=� �� View �� .P��� -   clmnExport_ID�� �� Width$ 0�� % !CExportDetails_xaml_clmnExport_ID#���   �� CellTemplate �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� 	Export_ID������ *ID+�� -   clmnisSelected��.���� .���� -   $��chk_CheckAllDetails�� �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� chkIsSelected�� d� 	IsChecked �� *
IsSelected+$ TwoWay=��� -   clmnExport_Date�� ' #CExportDetails_xaml_clmnExport_Date#���   �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Export_Date������ *Date+�� -   clmnExport_Type�� ' #CExportDetails_xaml_clmnExport_Type#���   �� T9��.R�� G0��$
��Auto��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Export_Type������ *
ExportType+�� -	   clmnExport_Status�� ) %CExportDetails_xaml_clmnExport_Status#���   �� T9��.R�� G0��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� Export_Status������ *Status+�� -
   $��btn_Refresh��$��0��$��2��$��Center=�btn_Refresh��  
BMC_Button#���   # CExportDetails_xaml_btn_Refresh#���  $	��137��$��55��$	��Top=��� -   $��lblExportType��$��1��$��2��$
��Bold"�$��18(�$��Segoe UI)�
�������$��Center=�$
��Left=�lblExportType�� $	  CExportDetails_xaml_lbStatusType#��� 	 $��3��$��	0,11,0,10q�$	��260���� -   $��cmbStatusType��$��2��$��2��$��Center=�$��Right=� �� DisplayMemberPath$ Description�� �� SelectedValuePath$
 Type�� 
 BMC_ComboBoxStyle#��� 
 $��30��$��2��$��
0,13,30,12q�$��98���� -   $��
btn_Update��$��4��$��2��$��Center=�
btn_Update��#���   " CExportDetails_xaml_btn_Update#���  $	��137��$��55��$	��Top=�� -    �Name$ pgBarExportDetails��pgBarExportDetails��$��Center=�$��Bottom=� � Minimum$ 0Q� � Maximum$	 100Q�$��20��$	��290��$��1��$��3��$��3���� -   $ txtPGStatusExportDetails��txtPGStatusExportDetails��$
��Bold"�$��Segoe UI)�$��14(�$��Center=�$	��Top=�$��1��$��4��$��3��
�������