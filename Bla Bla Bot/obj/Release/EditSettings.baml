   M S B A M L   `   `   `  ���� F  BBla Bla Bot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Bla_Bla_BotBla_Bla_Bot      Bla_Bla_Bot.EditSettings   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Bla_Bla_Bot     -     � Title$B  <Редактирование настроек сервера��$��580.997��$	��800�� � Icon$ 	Acorn.ico��.��� .+��� -   $��Отмена��$
��Left=�$��707,520,0,0q�$	��Top=�$��75���� -   $��Сохранить��$
��Left=�$��627,520,0,0q�$	��Top=�$��75���� -    ��Name$ 	ActiveBot��$��Бот активен?��$
��Left=�$��	10,10,0,0q�$	��Top=��� $0��*Анти флуд упоминаниями��$
��Left=�$	��155��$��	10,30,0,0q�$	��Top=�$	��189��.��� .+��� -   $ antifloodActive��$��Активность��$��Right=�$	��Top=�$��0,0,95,0q��� -    ��Name$ antifloodinminute��$
��Left=�$��23��$��0,20,0,0q� �� TextWrapping$
 Wrap=�$��5��$	��Top=�$	��105��{�.  �� $��в минуту��$
��Left=�$��
110,17,0,0q�$	��Top=��� -    ��Name$ antiflooddo��$��Действие��$
��Left=�$��84��$��0,48,0,0q�$	��Top=�$	��177��{�.  .��� .+�	� -    	�Name$ DoMentionFloodKick��$
��Kick��$
��Left=�$	��Top=�	� -	   $ DoMentionFloodBan��$	��Ban��$
��Left=�$	��Top=�$��0,15,0,0q�	� -
   $ DoMentionFloodWarn��$0��*Предупредить и удалить��$
��Left=�$	��Top=�$��0,30,0,0q�	� -   $ DoMentionFloodMute��$
��Mute��$
��Left=�$	��Top=�$��0,45,0,0q��� $��Анти флуд��$
��Left=�$	��334��$��
10,185,0,0q�$	��Top=�$	��189��.��� .+��� -   $ antifloodActive1��$��Активность��$	��Top=�$��0,0,95,0q�$��Right=�$��82���� -   $ antifloodinminute1��$
��Left=�$��23��$��0,20,0,0q�$
 Wrap=�$��5��$	��Top=�$	��105��{�.  �� $��в минуту��$
��Left=�$��
110,17,0,0q�$	��Top=��� -   $ antiflooddo1��$��Действие��$
��Left=�$��83��$��0,48,0,0q�$	��Top=�$	��177��{�.  .��� .+�	� -   $ DoAntiFloodKick��$
��Kick��$
��Left=�$	��Top=�	� -   $ DoAntiFloodBan��$	��Ban��$
��Left=�$	��Top=�$��0,15,0,0q�	� -   $ DoAntiFloodWarn��$0��*Предупредить и удалить��$
��Left=�$	��Top=�$��0,30,0,0q� d� 	IsChecked$
 TrueY�	� -   $ DoAntiFloodMute��$
��Mute��$
��Left=�$��0,45,0,0q��� -   $ 
exceptions��$��Исключения��$
��Left=�$	��179��$��	0,131,0,0q�$	��Top=�$	��177��{�.  .��� .+��� -    ��Name$ listchannels��$
��Left=�$	��Top=�$��73���� -   $��Доб.��$
��Left=�$��78,0,0,0q�$	��Top=�$��42��$��22���� -   	 ��Name$	 listexceptionchannels��$
��Left=�$	��129��$��0,27,0,0q�$	��Top=�$	��162���� -   $	��Del��$
��Left=�$��
125,0,-2,0q�$	��Top=�$��42��$��22���� $#��Отправка эмоций��$
��Left=�$	��303��$��	204,8,0,0q�$	��Top=�$	��200��.��� $��0,0,-2,0q�.+��� -   $ SendEmoteActive��$��Активность��$
��Left=�$	��Top=��� -   $ SendEmoteAdd��$��Добавление��$
��Left=�$	��127��$��0,15,0,0q�$	��Top=�$	��190��{�.  .��� .+��� -   $ SendEmoteCommand��$
��Left=�$��23��$
 Wrap=�$	��Top=�$	��180��$��0,0,-2,0q�$��Команда���� -   $ SendEmoteText��$
��Left=�$��23��$
 Wrap=�$	��Top=�$	��180��$��	0,28,-2,0q�$��Текст поиска���� -   $��Добавить��$
��Left=�$��0,86,0,0q�$	��Top=�$	��178���� -   $ SendEmoteTextAns��$
��Left=�$��23��$
 Wrap=�$	��Top=�$	��180��$��	0,56,-2,0q�$��Текст вывода���� -   
 ��Name$
 SendEmoteDelete��$��Удалить��$
��Left=�$��	0,147,0,0q�$	��Top=�$	��190��{�.  �� -   $	 SendEmoteList��$
��Left=�$	��107��$��	0,172,0,0q�$	��Top=�$	��190��{�.  �� $��Роль мута��$
��Left=�$��51��$��204,311,0,0q�$	��Top=�$	��200��.���� -    $ MuteRole��$
��Left=�$��0,3,-2,0q�$	��Top=�$	��190���� -!   $ exceptions_Copy��$��Защита ролей��$
��Left=�$	��179��$��204,362,0,0q�$	��Top=�$	��200��.��� .+��� -"   $ GuardRolesList��$
��Left=�$	��Top=�$��94��$��0,15,0,0q�{�.  �� -#   $
 GuardRolesAdd��$��Доб.��$
��Left=�$��	99,15,0,0q�$	��Top=�$��42��$��22��{�.  �� -$   $	 GuardRolesListAdded��$
��Left=�$	��114��$��0,42,0,0q�$	��Top=�$	��188��{�.  �� -%   $
 GuardRolesDel��$	��Del��$
��Left=�$��
146,15,0,0q�$	��Top=�$��42��$��22��{�.  �� -&   $ GuardRolesActive��$��Активность��$
��Left=�$	��Top=�