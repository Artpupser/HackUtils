# HackUtils
### Здесь будут: ассеты, подушки, шаблоны, конфиги, сниппеты и еще много разной фигни которая может пригодится (например плейлист КИШ)

### Манипуляции

- ```git add . ``` Добавить файлы в комит
- ```git commit -m название комита" -m "описание комита"``` Создать комит
- ```git push``` Запушить изменения
- ```git pull``` Подтянуть изменения
- ```git reset``` Убрать файлы из комита

### Необходимые приложения на Windows/Linux/MacOs
- Docker Desktop
- Git
- PostgreSql (pgAdmin/dbeaver)


### Миграция

```shell
dotnet tool install --global dotnet-ef
dotnet ef migrations add initial -s RyazanTrip.App -p RyazanTrip.DataAccess.Postgres
dotnet ef migrations add InitialCreate
dotnet ef database update -s ..\RyazanTrip.App\

```
### Запуск
```shell
docker compose up --build
```