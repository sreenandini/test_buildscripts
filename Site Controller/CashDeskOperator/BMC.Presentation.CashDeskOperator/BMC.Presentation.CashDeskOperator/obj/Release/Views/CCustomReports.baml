   M S B A M L   `   `   `  ����   BMC.Presentation.POS%    BMC.Presentation.CCustomReports   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :Custom-http://schemas.microsoft.com/wpf/2008/toolkit  P LAvalonControlsLibrary, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullJCustom1<http://schemas.AvalonControls/AvalonControlsLibrary/Controls  :d2http://schemas.microsoft.com/expression/blend/2008  -   $	��850��$	��750��  � 	Resources  ��%    z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�.��� -   $��gridSetting��gridSetting��$	��750��.+��� -    ��Name$ 	txtHeader��	txtHeader��$��18,14,350,0q�	 �� TextAlignment$
	 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$
��Left=�
 J� RenderTransformOrigin$
 0.5,0.5&� !  CCustomReports_xaml_txtHeader#���   ���	#FFFFFFFFF� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� Border_3��$��Stretch=�$��Stretch=� �� CornerRadius$ 5,5,5,5�����	#19CDDD98��  % !CCustomReports_xaml_txtReportName#���  $	��Top=�$��110,80,550,0q�$��16(����	#FFFFFFFF$��40��$
��Wrap=��� -   $��
cmbReports��
cmbReports��$��33��$��
130,75,0,0q�$��16(�$	��350��  BMC_ComboBoxStyle#���  $	��Top=��� 
GroupBox_1��$	��650��$��Segoe UI)�$��16(�$��Normal"�$	��400��$��100,160,0,0q�$
��Left=�$	��Top=����  � ColumnDefinitions �� col1��$	��267 ��� col2��$��* �.+���  , (CCustomReports_xaml_txtSelectionCriteria#���  $��10,0,0,0q�$��0��$
��BOLD"�$
��Left=��� $�� ��$��50,0,0,0q�$��1��$
��BOLD"�$
��Left=�� -    �Name$&  VoucherCouponLiabilityReportGrid��$	��400��$ VoucherCouponLiabilityReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � � RowDefinitions �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 �.+���  ( $CCustomReports_xaml_txtIssueDateTime#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��$
��Wrap=�(� -    (�Name$ vbIssueDate��vbStartDate��$��35��$	��175��$��Center=� (� Stretch$
 Fill=�$
��Left=�$��0��$��2��.��+  %Microsoft.Windows.Controls.DatePicker  -     Name$ dtpIssueDate_liability��dtpIssueDate_liability��$	��120��$��25��  CalendarStyle   CalendarStyle# �  $��Segoe UI)�$��Center=�$��12(�   IsTodayHighlighted .    TextBoxGradient#���  $	��Top=�$��0��$��2��{�. 2  ,AC.AvalonControlsLibrary.Controls.TimePicker  -     Name$ tmpIssueDate_liability��tmpIssueDate_liability��$��	110,0,0,0q�  TimePickerStyle#���  $��Center=�$	��155��$��36��#���    TextBoxOutline#���  $��1,1,1,1q�$��16(�$��0��$��2����  %	 !CCustomReports_xaml_txtDeviceType#��� 	 $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��$
��Wrap=��� -	   $��cmbDeviceType_liability��cmbDeviceType_liability��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��1��$��2����  (
 $CCustomReports_xaml_txtVoucherStatus#��� 
 $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0��$
��Wrap=��� -
   $ ��cmbVoucherStatus_liability��cmbVoucherStatus_liability��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2��� -   $$ ExpiredVoucherCouponReportGrid��$	��400��"ExpiredVoucherCouponReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 �.+���  ' #CCustomReports_xaml_txtFromDateTime#���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -   $ vbFromDate_Expired��vbFromDate_Expired��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -   $ dtpFromDate_Expired��dtpFromDate_Expired��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -   $ tmpFromDate_Expired��tmpFromDate_Expired��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2����  % !CCustomReports_xaml_txtToDateTime#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -   $ vbToDate_Expired��vbToDate_Expired��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -   $ dtpToDate_Expired��dtpToDate_Expired��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -   $ tmpToDate_Expired��tmpToDate_Expired��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2���� #��� 	 $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0��$
��Wrap=��� -   $��cmbDeviceType_Expired��cmbDeviceType_Expired��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2��� -   $&  RedeemedTicketByDeviceReportGrid��$	��400��$ RedeemedTicketByDeviceReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -   $ 
vbFromDate��
vbFromDate��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -   $ dtpFromDate_Redeem��dtpFromDate_Redeem��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -   $ tmpFromDate_Redeem��tmpFromDate_Redeem��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -   $ vbToDate��vbToDate��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -   $ dtpToDate_Redeem��dtpToDate_Redeem��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -   $ tmpToDate_Redeem��tmpToDate_Redeem��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2���� #��� 	 $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0��$
��Wrap=��� -   $��cmbDeviceType_Redeem��cmbDeviceType_Redeem��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2��� -   $ ExpenseDetailsReportGrid��$	��400��ExpenseDetailsReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� Col1��$	��350 ��� Col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 �.+���   CCustomReports_xaml_txtDate#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -   $ vbExpenseDetailReportDate��vbExpenseDetailReportDate��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -   $! dtpReportDate_ExpenseDetail��dtpReportDate_ExpenseDetail��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2����  ! CCustomReports_xaml_txtPeriod#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0���� -   $��cmbExpensePeriod��cmbExpensePeriod��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��1��$��2���� -   $��chkIsGamingDay��chkIsGamingDay��$��10,0,0,0q� $  CCustomReports_xaml_chkGamingDay#���  $��1��$��2�� d� 	IsChecked$ FalseY�$��32��$��Center=�
���   �$��Segoe UI)�$��16(�$��Normal"�$��Center=�� -    $" JackpotSlipSummaryReportGrid��$	��400�� JackpotSlipSummaryReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� Col1��$	��350 ��� Col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -!   $ vbReportStartDate��vbReportStartDate��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -"   $+ %dtpReportStartDate_JackpotSlipSummary��)%dtpReportStartDate_JackpotSlipSummary��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -#   $ tpReportStartTime��tpReportStartTime��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -$   $ vbReportEndDate��vbReportEndDate��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -%   $) #dtpReportEndDate_JackpotSlipSummary��'#dtpReportEndDate_JackpotSlipSummary��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��1��$��2��  -&   $ tpReportEndTime��tpReportEndTime��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2���� -'   $��chkIncludeHandpay��chkIncludeHandpay�� ) %CCustomReports_xaml_chkIncludeHandpay#���  $��1��$ FalseY�
���   �$��Segoe UI)�$��16(�$��Normal"�$��Center=�$��349,0,193,-34q�$��35��$��Bottom=�$��2���� -(   $��chkIncludeJackpot��chkIncludeJackpot�� ) %CCustomReports_xaml_chkIncludeJackpot#���  $��1��$ FalseY�
���   �$��Segoe UI)�$��16(�$��Normal"�$��Center=�$��349,0,193,-95q�$��35��$��Bottom=�$��2��� -)   $ TicketIssuedReportGrid��$	��400��TicketIssuedReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 ��� Row3��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -*   $ vbFromDate_Issued��vbFromDate_Issued��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -+   $ dtpFromDate_Issued��dtpFromDate_Issued��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -,   $ tmpFromDate_Issued��tmpFromDate_Issued��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� --   $ vbToDate_Issued��vbToDate_Issued��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -.   $ dtpToDate_Issued��dtpToDate_Issued��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -/   $ tmpToDate_Issued��tmpToDate_Issued��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2����   CCustomReports_xaml_txtSlot#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0���� -0   $��cmbSlot_Issued��cmbSlot_Issued��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2��.@��� 
 � Tag$	 ALL��.��ALL�� #��� 
 $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��3��$��0��$
��Wrap=��� -1   $��cmbVoucherStatus_Issued��cmbVoucherStatus_Issued��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��3��$��2��.@��� $	 ALL��.��ALL�� $ ACTIVE��.��ACTIVE�� $ 	CANCELLED��.��	CANCELLED�� $ EXPIRED��.��	EXPIRED�� $
 PAID��.��PAID�� $ PARTIALLYPAID��.��PARTIALLYPAID�� $
 VOID��.��VOID� -2   $ MeterListReportGrid��$	��400��MeterListReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� Col1��$	��350 ��� Col2��$	��425 � �� Row1��$��75 �.+���  " CCustomReports_xaml_txtAssetNo#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0���� -3   $��cmbAssetNumber��cmbAssetNumber��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2�� �� DisplayMemberPath$ StockNo�� �� SelectedValuePath$ InstallationNo��� -4   $&  ExceptionVoucherDetailReportGrid��$	��400��$ ExceptionVoucherDetailReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 ��� Row4��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -5   $ vbFromDate_Exception��vbFromDate_Exception��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -6   $ dtpFromDate_Exception��dtpFromDate_Exception��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -7   $ tmpFromDate_Exception��tmpFromDate_Exception��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -8   $ vbToDate_Exception��vbToDate_Exception��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -9   $ dtpToDate_Exception��dtpToDate_Exception��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -:   $ tmpToDate_Exception��tmpToDate_Exception��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2���� -;   $��chkDropBased��chkDropBased��$��10,0,0,0q� $  CCustomReports_xaml_chkDropBased#���  $��1��$��2��$ FalseY�$��32��$��Center=�
���   �$��Segoe UI)�$��16(�$��Normal"�$��Center=���  & "CCustomReports_xaml_txtBatchNumber#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��3��$��0���� -<   $��cmbBatchNumber��cmbBatchNumber��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��3��$��1��� -=   $5 /CrossPropertyLiabilityTransferSummaryReportGrid��$	��400��3/CrossPropertyLiabilityTransferSummaryReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 ��� Row3��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� ->   $ vbFromDate_TSR��vbFromDate_TSR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -?   $ dtpFromDate_TSR��dtpFromDate_TSR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -@   $ tmpFromDate_TSR��tmpFromDate_TSR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -A   $ vbToDate_TSR��vbToDate_TSR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -B   $ dtpToDate_TSR��dtpToDate_TSR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -C   $ tmpToDate_TSR��tmpToDate_TSR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2��� -D   $5 /CrossPropertyLiabilityTransferDetailsReportGrid��$	��400��3/CrossPropertyLiabilityTransferDetailsReportGrid��$��-35,215,0,0q�$	��Top=�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 ��� Row3��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -E   $ vbFromDate_TDR��vbFromDate_TDR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -F   $ dtpFromDate_TDR��dtpFromDate_TDR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -G   $ tmpFromDate_TDR��tmpFromDate_TDR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -H   $ vbToDate_TDR��vbToDate_TDR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -I   $ dtpToDate_TDR��dtpToDate_TDR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -J   $ tmpToDate_TDR��tmpToDate_TDR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2��� -K   $+ %CrossPropertyTicketAnalysisReportGrid��)%CrossPropertyTicketAnalysisReportGrid��$��-35,218,0,132q�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 ��� Row2��$��75 ��� Row3��$��75 ��� Row3��$��75 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -L   $ vbFromDate_TAR��vbFromDate_TAR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -M   $ dtpFromDate_TAR��dtpFromDate_TAR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2��  -N   $ tmpFromDate_TAR��tmpFromDate_TAR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -O   $ vbToDate_TAR��vbToDate_TAR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -P   $ dtpToDate_TAR��dtpToDate_TAR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�  -Q   $ tmpToDate_TAR��tmpToDate_TAR��$��	110,0,0,0q�#���  $��Center=�$	��155��$��36��#���  #���  $��1,1,1,1q�$��16(�$��1��$��2��� -R   $# StackerLevelDetailsReportGrid��!StackerLevelDetailsReportGrid��$��-35,218,0,132q�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��75 �.+���  ' #CCustomReports_xaml_txtStackerLevel#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0���� -S   $��cmbStackerLevel��cmbStackerLevel��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��0��$��1��� -T   $! AccountingWinLossReportGrid��AccountingWinLossReport��$��-35,218,0,132q�$y�Hidden=� �� col1��$	��350 ��� col2��$	��425 � �� Row1��$��65 ��� Row2��$��65 ��� Row3��$��65 ��� Row4��$��65 ��� Row5��$��65 �.+��� #���  $��Center=�$
��Left=�$��	205,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��0��$��0��(� -U   $ vbFromDate_AWLR��vbFromDate_AWLR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��0��$��2��.��  -V   $ dtpFromDate_AWLR��dtpFromDate_AWLR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=�$��0��$��2���� #���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��1��$��0��(� -W   $ vbToDate_AWLR��vbToDate_AWLR��$��35��$	��175��$��Center=�$
 Fill=�$
��Left=�$��1��$��2��.��  -X   $ dtpToDate_AWLR��dtpToDate_AWLR��$	��120��$��25��#���  # �  $��Segoe UI)�$��Center=�$��12(� .  $	��Top=���   CCustomReports_xaml_txtZone#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��2��$��0���� -Y   $��cmbZone_AWLR��cmbZone_AWLR��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��2��$��2����  # CCustomReports_xaml_txtCategory#���  $��Center=�$
��Left=�$��	215,0,0,0q�$��Segoe UI)�$��16(�$��Normal"�$��3��$��0���� -Z   $��cmbCategory_AWLR��cmbCategory_AWLR��$��30��$��16(�$	��175��#���  $��Center=�$
��Left=�$��3��$��2���� -[   $��chkNonCashable_AWLR��chkNonCashable_AWLR��$��30��
���   �$��Segoe UI)�$��16(�$��Normal"�$��Include Non Cashable��$��Center=�$ FalseY�$��4��$��1��� -\   $ 
buttonGrid��$��60��
buttonGrid��$��	0,580,0,0q�$	��Top=�.+��� -]   $��btnOK��  
BMC_Button#���  $��Center=�$
��Left=�$��	315,0,0,0q�$��55��  CCustomReports_xaml_btnOK#���  $��0��