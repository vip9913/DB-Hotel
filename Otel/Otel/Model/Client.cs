namespace Otel.Model
{
    class Client
    {
        public long id { get; private set; }
        public string client { get; private set; }
        public string  email { get; private set; }
        public string phone { get; private set; }
        public string address { get; private set; }
        public string   info { get; private set; }
        private MySQL sql;

        public Client(MySQL sql)
        {
            id = 0;
            client = "";
            email = "";
            phone = "";
            address = "";
            info = "";
            this.sql = sql;
        }
        
        /// <summary>
        /// Установка параметров клиента
        /// </summary>
        /// <param name="client">Установить пераметр</param>
        public void SetClient(string client)
        {
            this.client = client;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        /// <summary>
        ///                  Дополнительные комменты
        /// </summary>
        /// <param name="info">Установите дополнительные комменты</param>
        public void SetInfo(string info)
        {
            this.info = info;
        }

        /// <summary>
        /// Регистрация нового клиента
        /// </summary>
        public void InsertClient()
        {
            string query= "INSERT INTO Client (client, email, phone, address, info) " +
                 "VALUES ('"+sql.addslashes(client)+
                     "', '" + sql.addslashes(email)+
                     "', '" + sql.addslashes(phone)+
                     "', '" + sql.addslashes(address)+
                     "', '" + sql.addslashes(info)+"')";            
            do this.id=this.sql.Insert(query);
            while (this.sql.SqlError());
        }

 /*       ModelClient.SelectClient()	
	получение списка клиента

        SELECT* FROM Client;

    ModelClient.SelectClient(string find)

    получение списка клиента(по фильтру)

        SELECT* FROM Client ORDER BY Client;
        если в имени клиента есть буква О

        SELECT* FROM Client WHERE Client LIKE '%o%',
           OR email LIKE '%m%',
           OR id = '1675';
    
    ModelClient.SelectClient(int client_id)

    получение данных заданного клиента

        SELECT* FROM Client WHERE id='3';		

	ModelClient.UpdateClient(int client_id)	

    изменение данных клиента
        UPDATE Client
            SET client='POP',
			email='test@test.com',
			phone='543210',
			address='Luna',
			info='All Hi!'
			WHERE id = 4 limit 1;

	ModelCalendar.InsertDays(int year)

    генерация календаря на заданный год
    INSERT INTO Calendar SET day = '2019-01-04',
        wend = 0, holiday = 1;

        ModelCalendar.SetHoliday(string day)	

    ModelCalendar.DelHoliday(string day)	

    установка праздничных дней
    UPDATE Calendar SET holiday=TRUE
        WHERE day='2019-01-04';

	ModelRoom.SelectRooms()		
	получение списка комнат

    SELECT* FROM room ORDER By step;

        ModelRoom.InsertRoom()		
	добавлении новой комнаты
    INSERT INTO room

    SET room = "Double 102",
        beds = 1,
        floor = '1',
        info = 'No Nice bed';

        UPDATE Room SET step = 1 WHERE id = 2; помечает записи что бы не было одинаковых

       ModelRoom.SelectRoom(int room_id)

    получение информации по заданной комнате

    SELECT* FROM room WHERE id=1;

	ModelRoom.UpdateRoom(int room_id)		

    редактирование данных комнаты
    UPDATE room
    SET room="single 101",
		beds=1,
		floor='1',
		info ='Nice single bed'
		WHERE id = 1;

        ModelRoom.ShiftRoomUp(int room_id)			

    ModelRoom.ShiftRoomDn(int room_id)			
*	перемещение комнаты по списку вниз-вверх(сортировка)


    ModelBook.InsertBook()			
	создание новой регистрации
    INSERT INTO book

    SET client_id = 1,
    book_date = now(),
    from_day = '2019-01-01',
    till_day = '2019-01-01',
    adults = 1,
    childs = 0,
    status = 'wait',
    info = 'Not specified';

        ModelBook.UpdateStatus(int book_id, string status)			

    изменение статуса регистрации
            отмена

            подтверждение
            ожидание подтверждения

    UPDATE Book
        SET status='confirm'
		WHERE id = 1;

        ModelBook.UpdateBook(int book_id)					

    редактирование регистрации  без дней

    UPDATE book

    SET adults = 1,
    childs = 1,
    info = '-'

    WHERE id = 1;

        ModelBook.UpdateFromDay(int book_id, string from_day)			

    ModelBook.UpdateTillDay(int book_id, string till_day)			

    редактирование дней регистраций
    UPDATE book
    SET client_id=1,	
	from_day='2019-01-01',
	till_day='2019-01-01'
	WHERE id = 1;

        ModelBook.SelectBooks()			
	получение списка регистраций
    SELECT client_id, client, book_date, from_day, till_day, adults,childs, status,
	b.info
    FROM book b LEFT JOIN Client c

    ON b.client_id=c.id
    WHERE b.id= 1\G

    ModelBook.SelectBooks(string find)

    получение списка регистраций(по фильтру)

    SELECT client_id, client, book_date, from_day, till_day, adults, childs, status,
    b.info
    FROM book b LEFT JOIN Client c

    ON b.client_id=c.id
    WHERE Client LIKE '%O%'

    OR book_date LIKE 'P%'

    OR from_day LIKE 'P%'

    OR till_day LIKE 'P%'

    OR adults = '1'

    OR childs = '1'

    OR status = 'p'

    OR b.info LIKE '%O%';

        ModelMap.SelectMap(string from_day, string till_day)			

    получить занятость комнат на указанный месяц(карту Отель-гостиница)

    SELECT room_id, book_id, calendar_day, status, adults, childs

    FROM Map

    WHERE calendar_day BETWEEN '2019-01-01' AND '2019-12-31';
	WHERE LEFT(calendar_day, 7)='2019-12'//можно и так

	ModelMap.InsertMap()			
	Добавить запись в Map

    INSERT INTO Map
    SET room_id=1,
	book_id=1,
	calendar_day='2019-01-01',
	status='confirm',
	adults=1,
	childs=1;

	ModelMap.InitMap(int room_id, int book_id, string calendar_day)			

    ModelMap.UpdateMap()			
	Изменить запись в Map

    UPDATE Map

    SET status = '-',
    adults = 1,
    childs = 0

    WHERE room_id = 1

        AND book_id = 1

        AND calendar_day = '2019-01-01';

        ModelMap.DeleteMap(string room_id, string book_id)			

    Удалить запись в Map

    DELETE FROM Map
    WHERE

        room_id=1 AND book_id = 1 AND calendar_day = '2019-01-01';


*	Выселить всех


    ModelRoom.SelectFreeRooms(string from_day)			

    Получение списка свободных комнат на заданный день

    SELECT* FROM room
   WHERE id Not in (SELECT room_id FROM map WHERE calendar_day = '2019-01-02');
   */
    }
}
