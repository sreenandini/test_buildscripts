   M S B A M L   `   `   `  ����   BMC.Presentation.POS;    5BMC.Presentation.POS.UserControls.ManualCashDispenser   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  ManualCashDispenser��$	��600��$	��350��   MainScreenBG#���   $��Stretch=�$��Center=�$��Segoe UI)�$��14(� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterOwner=� � 	Resources  & "GroupBoxHeaderAdjustmentFromMargin �� 
TargetType �� Property �� Value �� 
TargetType	 J� SnapsToDevicePixels
 � ColumnDefinitions � RowDefinitions �� CornerRadius   PositionDetailsGroupBoxStyle ��Name �� Padding��%�  &	       �� Style_68��" ��.��� 
Setter_568�� � ��$ 1q��� 
Setter_569�� � �� �� ControlTemplate_75��" ��.R�� Grid_82��	 . 
 �� ColumnDefinition_40��$��6 ��� ColumnDefinition_41��$
��Auto ��� ColumnDefinition_42��$��* ��� ColumnDefinition_43��$��6 � �� RowDefinition_43��$
��Auto ��� RowDefinition_44��$
��Auto ��� RowDefinition_45��$��* ��� RowDefinition_46��$��6 �.+��� 	Border_31��$��4��$��0��$��4�����	#FF161826#��z��$ 4��#���  �� 	Border_32��$��0��$��4��$��1��$��3��#��z��
������ #��z��$ 4���� $ Header��
Header��$��Stretch=�$��0,5,0,0q�$��1��$��2��$��0��$��2�����	#FF161826$��1,1,1,1q�$ 2,2,2,2��$ 0,0,0,0q�#���  $��30��._��� ContentPresenter_5��#	 z	 $��Header����. $��Center=�$��9,0,0,0q��� ContentPresenter_6��#��z��#	 z	 $��1��$��2��$��2��.���� 	Border_10��$ 0,0,0,0�����	#19CDDD98$��0,0,0,0q�._���  " ManualCashDispenser_xaml_Title#���  $��16(�$
��bold"�#���  $��Stretch=�$��Stretch=�.��� -    �Name$ 
LayoutRoot��
LayoutRoot�����	#19FFFFFF J� RenderTransformOrigin$ 0.5,0.5&� �� $��* ��� $��80 �.+�� $��0��$��Stretch=�$��Stretch=�
 �� $��20 ��� $	��100 ��� $	��100 ��� $
��100* ��� $	��120 ��� $��20 � �� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��* �.+��� -    ��Name$ lblCaption1��lblCaption1��$��0��$��1��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"� �� TextAlignment$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=� ) %ManualCashDispenser_xaml_Lbl_Caption1#���  �� -   $ lblCaption2��lblCaption2��$��0��$��2��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=� ) %ManualCashDispenser_xaml_Lbl_Caption2#���  �� -   $ lblCaption3��lblCaption3��$��0��$��3��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=� ) %ManualCashDispenser_xaml_Lbl_Caption3#���  �� -   $ lblCaption4��lblCaption4��$��0��$��4��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=� ) %ManualCashDispenser_xaml_Lbl_Caption4#���  �� -   $ lblCD1��
lblCD1��$��1��$��1��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=��� -   $ lblBalance1��lblBalance1��$��1��$��2��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=��� -	    ��Name$ txtCD1��
txtCD1��$��1��$��3��$��5,10,5,0q� �� TextWrapping$
 Wrap=�  TextBoxStyle1#���   	 TextBoxGradient#��� 	  
 TextBoxOutline#��� 
  �� TextAlignment$ Right=�{�.  �� 
IsReadOnly .  $��25�� �� 	MaxLength$ 6���� -
   $ 
lblCD1Calc��
lblCD1Calc��$��1��$��4��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Right=�$��Center=��� -   $ lblCD2��
lblCD2��$��2��$��1��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,5,0,0q�$��Center=�$��Center=��� -   $ lblBalance2��lblBalance2��$��2��$��2��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=��� -   $ txtCD2��
txtCD2��$��2��$��3��$��5,5,5,0q�$
 Wrap=�#���  #��� 	 #��� 
 $ Right=�{�.  .  $��25��$ 6���� -   $ 
lblCD2Calc��
lblCD2Calc��$��2��$��4��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,5,0,0q�$��Right=�$��Center=��� -   $ lblTotal��lblTotal��$��3��$��3��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,0,0,0q�$��Right=�$��Center=� & "ManualCashDispenser_xaml_Lbl_Total#���  �� -   $ lblTotalCalc��lblTotalCalc��$��3��$��4��$
��Auto��$
��Auto��$��Segoe UI)�$��14(�$
��Bold"�$ Center=�$
��Wrap=�$��0,0,0,0q�$��Right=�$��Center=�� $��1��$��Stretch=�$��Stretch=�
 �� $	��50* ��� $	��150 ��� $	��150 ��� $	��50* �.+��� -   $��
btnProcess��$��Segoe UI)�$��14(�$��55�� ' #ManualCashDispenser_xaml_btnProcess#���    
BMC_Button#���  $��1��$��0��$��5,5,5,5q�$��Right=�$��Center=��� -   $��	btnCancel��$��Segoe UI)�$��14(�$��55�� & "ManualCashDispenser_xaml_btnCancel#���  #���  $��2��$��0��$��5,5,5,5q�$
��Left=�$��Center=�