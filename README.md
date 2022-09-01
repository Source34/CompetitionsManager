# CompetitionManager
## Author - Zvezdin Roman
Приложение для ведения информации по соревнованям.

Основновый функционал:
- CRUD для сущностей БД c UI на WPF
- Расчет метрик

Детали реализации:
- Стэк: C#, клиент - WPF, сервер - ASP.NET WebAPI., OpenAPI, MSSQL, EF Core
- REST-архитектура
- трехслойная серверная архитектура (Core, Data, Web)
- Unit-тестирование
- Паттерн репозиторий
- Db Codefirst migrations
- Класс-обертка API на клиенте сгенерирован при помощи OpenAPI
- Диаграмма БД: 







![Screenshot_3](https://user-images.githubusercontent.com/41762401/184559944-0d826b89-8a40-4462-a67f-27de538fc41f.png)


P.S. Реализация клиента на WPF выполнена в упрощенном виде, без использования MVVM.
