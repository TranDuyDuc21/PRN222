   M S B A M L   `   `   `  ����  Lab2    HttpClientApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  HttpClient WPF��5   	   $	��450��6    $	��800��6-   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5
      $
��Auto �6   .+�5   
   �� -   5        $��
UrlTextBox��5      $��0��6$   $��Stretch=�64   $��10q�6R   $	��Top=�6^   $��30��6v   $ �� HorizontalScrollBarVisibility$
 Auto=�6�   �� 5   
   -   5        $��FetchDataButton��5      $��1��6(   $��
Fetch Data��68   $��Center=�6M   $	��Top=�6j   $	��100��6�   $��10q�6�   $��30��6�   6�   �� 5   
   -   5        $��ClearButton��5      $��1��6$   $��Clear��64   $��Center=�6D   $	��Top=�6a   $	��100��6y   $��
10,60,10,0q�6�   $��30��6�   6�   �� 5   
   -   5        $��CloseButton��5      $��1��6$   $��Close��64   $��Center=�6D   $	��Top=�6a   $	��100��6y   $��10,110,10,0q�6�   $��30��6�   6�   �� 5   
   -   5        $��DisplayTextBox��5      $��0��6(   $��Stretch=�68   $��10,100,10,10q�6V   $��Stretch=�6l   $	��300��6�    �� 
IsReadOnly$
 True��6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   5      5      