# AdvertisementAPI

Вопросы и принятые решения по ним (Детали):

1) Хранение картинок - в задании не было информации по структуре базы данных, выбор стоял между хранением ссылок в отдельной таблице в разных строках и хранением в поле
основной таблицы с последующей сериализацией и десериализацией в/из формата JSON. Изначально реализовал отдельной таблицей, потом перечитал задание и из контекста 
задачи понял, что было было удобнее(но это не точно) реализовать хранение всех ссылок в одной строке.

Вопросы и принятые решения по ним (Усложнения):
1) Архитектура сервиса описана в виде текста и/или диаграмм - в процессе.
2) Документация: есть структурированное описание методов сервиса. - возникли вопросы по вариантам реализации. Выбрал Swagger, Summary.
 
