   M S B A M L   `   `   `  ����   BMC.Presentation.POS2    ,BMC.Presentation.POS.Views.CVaultDeclaration   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -   $	��863��$	��553��.��� .+��� -   $��grpUnDeclaredVaultDrop��grpUnDeclaredVaultDrop��$��8,8,8,15q�$��Segoe UI)�$��14(����� $��Segoe UI)�$��16(�$
��Bold"�$
��Auto��.����   �� Orientation$  
Horizontal=�$��Stretch=�.+��� $��5,-3,0,0q� .  *CVaultDeclaration_xaml_UnDeclaredVaultDrop#���   $
��Auto��.��� .+�� -    �Name$ grdAssetDetails��$��0��$��0,0,0,0q�$��60��$	��Top=�$��Stretch=� � RowDefinitions ��  � ColumnDefinitions �� $
��.75* ��� $��2* ��� $��* ��� $
��1.5* ��� $	��.5* ��� $��* �.+��� -    ��Name$ lblVault��$��0�� & "CVaultCashEntry_xaml_lbl_VaultName#���  $��Segoe UI)�$��14(�$
��Bold"�$��Stretch=�}�.   �� TextAlignment$ Center=�$��50��$��50��$��Center=�$��22��$
��0,22q��� -   $ lblManf��$��2�� ) %CVaultCashEntry_xaml_lbl_Manufacturer#���  $��Segoe UI)�$��14(�$
��Bold"�$��Stretch=�}�.  $ Center=�$��50��$��50��$��Center=�$��22��$
��0,22q��� -   $ lblTypePrefix��$��4�� ' #CVaultCashEntry_xaml_lbl_TypePrefix#���  $��Segoe UI)�$��14(�$
��Bold"�$��Stretch=�}�.  $ Center=�$��50��$��50��$��Center=�$��22��$
��0,22q��� -    ��Name$ txtVault��$��14(�$��1��txtVault��$��Center=� �� TextWrapping$ NoWrap=�  TextBoxDIsableGradient#���    TextBoxOutline#���    TextBoxForeground#���    TextBoxStyle1#���  $��Segoe UI)� �� 
IsReadOnly . }�.  $��Center=�$��Stretch=�	 �� TextAlignment$
	 Left=�$��25��$��	0,18,0,17q����� *	VaultName+�� -   $ txtManufacturer��$��14(�$��3��txtManufacturer��$��Center=�$ NoWrap=�#���  #���  #���  #���  $��Segoe UI)� . }�.  $��Center=�$��Stretch=�$
	 Left=�$��25��$��
10,18,0,17q����� *Manufacturer+�� -	   $ txtTypePrefix��$��14(�$	��110��$��5��txtTypePrefix��$��Center=�$ NoWrap=�#���  #���  #���  #���  $��Segoe UI)� . }�.  $��Center=�$
��Left=�$
	 Left=�$��	0,18,0,17q�$��25������ *
TypePrefix+� .+��� -
   
 ��Name$
 lstVaultDrop��lstVaultDrop��$��1��$��Visible=�$��	8,60,8,80q�$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��13(�$��Segoe UI)� �� SelectionMode$ Single=�  Bmc_ListViewStyleFixedLock#���   "	 BMC_ListView_ListViewItemStyle#��� 	  �� View �� g_vaultdeclaration��-   g_vaultdeclaration��.P���  #
 CVaultCashEntry_xaml_gvc_DropID#��� 
  �� Width$	 100�� �� CellTemplate �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Center=�$��0,0,0,0q�$
��Wrap=����� *	Drop_ID+�� $	 110������ ��    CVaultDeclaration_xaml_Fills#��   ��   App_CurrencyCulture#��   �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Right=�$��0,0,0,0q�$
��Wrap=����� *
FillAmount+�� $	 100������ ��    CVaultDeclaration_xaml_Bleed#��   �� #��   �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Right=�$��0,0,0,0q�$
��Wrap=����� *BleedAmount+�� $	 105������ ��  & "CVaultDeclaration_xaml_Adjustments#��   �� #��   �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Right=�$��0,0,0,0q�$
��Wrap=����� *AdjustmentAmount+�� $	 110������ ��  # CVaultDeclaration_xaml_BMCTotal#��   �� #��   �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Right=�$��0,0,0,0q�$
��Wrap=����� *Meter_Balance+�� $	 110������ ��  % !CVaultDeclaration_xaml_VaultTotal#��   �� #��   �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Right=�$��0,0,0,0q�$
��Wrap=����� *Vault_Balance+��  ' #CVaultDeclaration_xaml_DropDatetime#���  $	 170�� �� .R�� .+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$ Center=�$��0,0,0,0q����� *CreatedDate+� -   $ grdDeclaration��$��2��$��Bottom=�$��Stretch=� ��  �� $
��128* ��� $	��30* �.+��� -   $��btnPrint��btnPrint��$��0��$��Right=�   CVaultDeclaration_xaml_Print#���  $��0,0,20,7q�  
BMC_Button#���  $��58��$��Bottom=�$	��128���� -   $��
btnDeclare��
btnDeclare��$��1��$��Right=� " CVaultDeclaration_xaml_Declare#���  $��0,0,20,7q�#���  $��58��$��Bottom=�$	��128��