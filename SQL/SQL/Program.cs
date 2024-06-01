using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SQL
{
    internal class Program
    {
        static string connectingString = @"
            Host = localhost; 
            Username = postgres; 
            Password = Admin123; 
            Database = Minecraft";

        /* Создание и Обновление
         * - Добавить или изменить материал
         * - Добавить или изменить предмет
         * - Добавить или изменить описание предмета
         * Чтение
         * - Найти материал
         * - Найти предмет
         * - Найти описание предмета
         * - Найти все материалы
         * - Найти все предметы
         * - Найти все описания предметов
         * Удаление
         * - Удалить материал
         * - Удалить предмет
         * - Удалить описание предмета
         * - Удалить все материалы
         * - Удалить все предметы
         * - Удалить все описания предметов
         * - Удалить все!
         */

        /*
         * 
         *   ЛИСТАЙ ВНИЗ, ТАМ СТРУКТУРА БАЗЫ ДАННЫХ (чтоб понятнее было)
         * 
         */

        static void Main(string[] args)
        {
            Delete();
            Fill();

            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Чем займемся сейчас?");
                Console.WriteLine("1 - Создание и Обновление");
                Console.WriteLine("2 - Чтение");
                Console.WriteLine("3 - Удаление");
                Console.WriteLine("4 - Выход");

                Console.Write("Ваш выбор: ");
                int action = int.Parse(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine();
                Console.WriteLine();

                switch (action)
                {
                    case 1:
                        Action_1();
                        break;

                    case 2:
                        Action_2();
                        break;

                    case 3:
                        Action_3();
                        break;

                    case 4:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }

                Console.WriteLine();
            }
        }

        // Действия

        static void Action_1() // Для создания и обновления
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Что будем делать?");
            Console.WriteLine("1 - Добавить или изменить материал");
            Console.WriteLine("2 - Добавить или изменить предмет");
            Console.WriteLine("3 - Добавить или изменить описание предмета");

            Console.Write("Ваш выбор: ");
            int action = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            switch (action)
            {
                case 1:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Введите название: ");
                        string name = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        FillMaterial(id, name);
                    }
                    break;

                case 2:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Введите название: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите id материалов через пробел: ");
                        string[] materials_string = Console.ReadLine().Split(' ');
                        int[] materials = new int[materials_string.Length];
                        for (int i = 0; i < materials_string.Length; i++)
                            materials[i] = int.Parse(materials_string[i]);

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        FillObject(id, name, materials);
                    }
                    break;

                case 3:
                    {
                        Console.Write("Введите название: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите описание: ");
                        string description = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        FillObjectDescription(name, description);
                    }
                    break;

                default:
                    Console.WriteLine("Такого действия нет");
                    break;
            }
        }

        static void Action_2() // Для чтения
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Что будем делать?");
            Console.WriteLine("1 - Найти материал");
            Console.WriteLine("2 - Найти предмет");
            Console.WriteLine("3 - Найти описание предмета");
            Console.WriteLine("4 - Найти все материалы");
            Console.WriteLine("5 - Найти все предметы");
            Console.WriteLine("6 - Найти все описания предметов");

            Console.Write("Ваш выбор: ");
            int action = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            switch (action)
            {
                case 1:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadMaterial(id);
                    }
                    break;

                case 2:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadObject(id);
                    }
                    break;

                case 3:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadObjectDescription(id);
                    }
                    break;

                case 4:
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadMaterials();
                    }
                    break;

                case 5:
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadObjects();
                    }
                    break;

                case 6:
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        ReadObjectDescriptions();
                    }
                    break;

                default:
                    Console.WriteLine("Такого действия нет");
                    break;
            }
        }

        static void Action_3() // Для удаления
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Что будем делать?");
            Console.WriteLine("1 - Удалить материал");
            Console.WriteLine("2 - Удалить предмет");
            Console.WriteLine("3 - Удалить описание предмета");
            Console.WriteLine("4 - Удалить все материалы");
            Console.WriteLine("5 - Удалить все предметы");
            Console.WriteLine("6 - Удалить все описания предметов");
            Console.WriteLine("7 - Удалить все!");

            Console.Write("Ваш выбор: ");
            int action = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            switch (action)
            {
                case 1:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        DeleteMaterial(id);
                    }
                    break;

                case 2:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        DeleteObject(id);
                    }
                    break;

                case 3:
                    {
                        Console.Write("Введите id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        DeleteObjectDescription(id);
                    }
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    DeleteMaterials();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    DeleteObjects();
                    break;

                case 6:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    DeleteObjectDescriptions();
                    break;

                case 7:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Delete();
                    break;

                default:
                    Console.WriteLine("Такого действия нет");
                    break;
            }
        }

        // Заполнение

        static void Fill() // Заполняю каждую из 3 таблиц начальными значениями
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"
                    INSERT INTO materials (id_material, name_material) 
                    VALUES 
                    (1, 'палка'), 
                    (2, 'доска'), 
                    (3, 'булыжник'), 
                    (4, 'железо'), 
                    (5, 'алмаз')"
                    , conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand(@"
                    INSERT INTO objects (id_object, name_object, materials) 
                    VALUES 
                    (1, 'деревянная кирка', ARRAY[1, 1, 2, 2, 2]), 
                    (2, 'каменная кирка', ARRAY[1, 1, 3, 3, 3]), 
                    (3, 'железная кирка', ARRAY[1, 1, 4, 4, 4]), 
                    (4, 'проигрыватель', ARRAY[2, 2, 2, 2, 2, 2, 2, 2, 5]), 
                    (5, 'железный блок', ARRAY[4, 4, 4, 4, 4, 4, 4, 4, 4]), 
                    (6, 'камень', ARRAY[3])"
                    , conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand(@"
                    INSERT INTO object_descriptions (name_object, description) 
                    VALUES 
                    ('деревянная кирка', 'инструмент, позволяет копать блоки, очень медленная'), 
                    ('каменная кирка', 'инструмент, позволяет копать блоки, медленная'), 
                    ('железная кирка', 'инструмент, позволяет копать блоки, достаточно быстрая'), 
                    ('проигрыватель', 'предмет интерьера, создает музыку'), 
                    ('железный блок', 'твердый блок из железных слитков'), 
                    ('камень', 'декоративный блок, используется для создания кирпичей')"
                    , conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void FillMaterial(int id, string name) // Добавляю или обновляю материал
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( // Получить количество материалов с таким id
                    $"SELECT COUNT(*) FROM materials WHERE id_material = {id}"
                    , conn))
                {
                    int count = int.Parse(cmd.ExecuteScalar().ToString());

                    if (count > 0) // Уже есть? Обновляем
                    {
                        using (var updateCmd = new NpgsqlCommand(
                            $"UPDATE materials SET name_material = '{name}' WHERE id_material = {id}"
                            , conn))
                        {
                            updateCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Поле успешно обновлено");
                    }
                    else // Еще нет? Создаем новый
                    {
                        using (var insertCmd = new NpgsqlCommand(
                            $"INSERT INTO materials (id_material, name_material) VALUES ({id}, '{name}')"
                            , conn))
                        {
                            insertCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Поле успешно создано");
                    }
                }
            }
        }

        static void FillObject(int id, string name, int[] materials) // Так же, как с материалом, только с предметом
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();
                string materialsArray = $"ARRAY[{string.Join(",", materials.Select(m => m.ToString()))}]";

                using (var cmd = new NpgsqlCommand(
                    $"SELECT COUNT(*) FROM objects WHERE id_object = {id}"
                    , conn))
                {
                    int count = int.Parse(cmd.ExecuteScalar().ToString());

                    if (count > 0)
                    {
                        using (var updateCmd = new NpgsqlCommand(
                            $"UPDATE objects SET name_object = '{name}', materials = {materialsArray} WHERE id_object = {id}"
                            , conn))
                        {
                            updateCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Объект успешно обновлен");
                    }
                    else
                    {
                        using (var insertCmd = new NpgsqlCommand(
                            $"INSERT INTO objects (id_object, name_object, materials) VALUES ({id}, '{name}', {materialsArray})"
                            , conn))
                        {
                            insertCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Объект успешно создан");
                    }
                }
            }
        }

        static void FillObjectDescription(string name, string description) // Так же, как с материалом, только с описанием
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    $"SELECT COUNT(*) FROM object_descriptions WHERE name_object = '{name}'"
                    , conn))
                {
                    int count = int.Parse(cmd.ExecuteScalar().ToString());

                    if (count > 0)
                    {
                        using (var updateCmd = new NpgsqlCommand(
                            $"UPDATE object_descriptions SET description = '{description}' WHERE name_object = '{name}'"
                            , conn))
                        {
                            updateCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Описание успешно обновлено");
                    }
                    else
                    {
                        using (var insertCmd = new NpgsqlCommand(
                            $"INSERT INTO object_descriptions (name_object, description) VALUES ('{name}', '{description}')"
                            , conn))
                        {
                            insertCmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Описание успешно создано");
                    }
                }
            }
        }

        // Чтение

        static void ReadMaterial(int id_material) // Найти материал по ид
        {
            string name = string.Empty;

            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                // Получить name_material из materials по ид
                using (var cmd = new NpgsqlCommand($"SELECT name_material FROM materials WHERE id_material = {id_material}", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Прочитался? Берем значение
                        {
                            name = reader.GetString(0);
                        }
                        else
                        {
                            name = "неизвестный материал";
                        }
                    }
                }
            }

            Console.WriteLine($"ID = {id_material}: {name}");
        }

        static void ReadObject(int id_object) // Найти предмет по ид
        {
            string name = string.Empty;
            string description = string.Empty;
            List<int> materialsIndexes = new List<int>();
            int unknown = 0;

            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                // Ищем в предметах название предмета по ид
                using (var cmd = new NpgsqlCommand($"SELECT name_object FROM objects WHERE id_object = {id_object}", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Нашли? Сохранили
                        {
                            name = reader.GetString(0);
                        }
                        else
                        {
                            Console.WriteLine($"ID = {id_object}: неизвестный предмет");
                            return;
                        }
                    }
                }

                // По этому имени ищем описание
                using (var cmd = new NpgsqlCommand($"SELECT description FROM object_descriptions WHERE name_object = '{name}'", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Нашли? Сохранили
                        {
                            description = reader.GetString(0);
                        }
                        else
                        {
                            description = "неизвестно";
                        }
                    }
                }

                // По ид ищем материалы для крафта
                using (var cmd = new NpgsqlCommand($"SELECT materials FROM objects WHERE id_object = {id_object}", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var materialsArray = reader.GetFieldValue<int[]>(0);
                            foreach (var index in materialsArray)
                            {
                                materialsIndexes.Add(index);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"ID = {id_object}: {name}");
            Console.WriteLine($"Описание: {description}");
            Console.WriteLine("Крафт:");
            foreach (var index in materialsIndexes.Distinct())
            {
                int count = materialsIndexes.Count(i => i == index);

                string materialName = string.Empty;

                using (var conn = new NpgsqlConnection(connectingString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand($"SELECT name_material FROM materials WHERE id_material = {index}", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                materialName = reader.GetString(0);
                                Console.WriteLine($"- {materialName} x{count}");
                            }
                            else
                            {
                                unknown += count;
                            }
                        }
                    }
                }
            }

            if (unknown > 0)
            {
                Console.WriteLine($"- unknown x{unknown}");
            }
        }

        static void ReadObjectDescription(int id_object_description) // Найти предмет по описанию (как с материалом)
        {
            string name = string.Empty;

            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand($"SELECT name_object FROM objects WHERE id_object = {id_object_description}", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader.GetString(0);
                        }
                        else
                        {
                            Console.WriteLine($"ID = {id_object_description}: нет описания");
                            return;
                        }
                    }
                }

                using (var cmd = new NpgsqlCommand($"SELECT description FROM object_descriptions WHERE name_object = '{name}'", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader.GetString(0);
                        }
                        else
                        {
                            Console.WriteLine($"ID = {id_object_description}: нет описания");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine($"ID = {id_object_description}: {name}");
        }

        static void ReadMaterials() // Прочитать все материалы, которые есть (вот тебе и массив, без лишних объектов)
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT id_material, name_material FROM materials", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool have_values = false;
                        while (reader.Read())
                        {
                            int idObject = Convert.ToInt32(reader["id_material"]);
                            string nameObject = reader["name_material"].ToString();
                            have_values = true;
                            Console.WriteLine($"ID = {idObject}: {nameObject}");
                        }

                        if (!have_values) Console.WriteLine("Таблица пустая");
                    }
                }
            }
        }

        static void ReadObjects() // Прочитать все предметы
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT id_object, name_object FROM objects", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool have_values = false;
                        while (reader.Read())
                        {
                            int idObject = Convert.ToInt32(reader["id_object"]);
                            string nameObject = reader["name_object"].ToString();
                            have_values = true;
                            Console.WriteLine($"ID = {idObject}: {nameObject}");
                        }

                        if (!have_values) Console.WriteLine("Таблица пустая");
                    }
                }
            }
        }

        static void ReadObjectDescriptions() // Прочитать все описания
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand("SELECT name_object, description FROM object_descriptions", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool have_values = false;
                        while (reader.Read())
                        {
                            string idObject = reader["name_object"].ToString();
                            string nameObject = reader["description"].ToString();
                            have_values = true;
                            Console.Write($"{idObject}:");
                            for (int i = idObject.Length; i < 20; ++i) Console.Write(" ");
                            Console.WriteLine(nameObject);
                        }

                        if (!have_values) Console.WriteLine("Таблица пустая");
                    }
                }
            }
        }

        // Удаление

        static void DeleteMaterial(int id_material) // Удалить материал
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var selectObjectCmd = new NpgsqlCommand($"SELECT id_material FROM materials WHERE id_material = {id_material}", connection))
                {
                    using (var objectReader = selectObjectCmd.ExecuteReader())
                    {
                        if (objectReader.Read())
                        {
                            objectReader.Close();

                            using (var deleteObjectCmd = new NpgsqlCommand($"DELETE FROM materials WHERE id_material = {id_material}", connection))
                            {
                                deleteObjectCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Объект не найден");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void DeleteObject(int id_object) // Удалить предмет
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var selectObjectCmd = new NpgsqlCommand($"SELECT id_object FROM objects WHERE id_object = {id_object}", connection))
                {
                    using (var objectReader = selectObjectCmd.ExecuteReader())
                    {
                        if (objectReader.Read())
                        {
                            objectReader.Close();

                            using (var deleteObjectCmd = new NpgsqlCommand($"DELETE FROM objects WHERE id_object = {id_object}", connection))
                            {
                                deleteObjectCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Объект не найден");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void DeleteObjectDescription(int id_description) // Удалить описание
        {
            using (var connection = new NpgsqlConnection(connectingString))
            {
                connection.Open();

                using (var selectObjectCmd = new NpgsqlCommand($"SELECT name_object FROM objects WHERE id_object = {id_description}", connection))
                {
                    using (var objectReader = selectObjectCmd.ExecuteReader())
                    {
                        if (objectReader.Read())
                        {
                            string name = objectReader["name_object"].ToString();
                            objectReader.Close();

                            using (var selectDescriptionCmd = new NpgsqlCommand($"SELECT * FROM object_descriptions WHERE name_object = '{name}'", connection))
                            {
                                using (var descriptionReader = selectDescriptionCmd.ExecuteReader())
                                {
                                    if (descriptionReader.Read())
                                    {
                                        name = descriptionReader["name_object"].ToString();
                                        descriptionReader.Close();

                                        using (var deleteDescriptionCmd = new NpgsqlCommand($"DELETE FROM object_descriptions WHERE name_object = '{name}'", connection))
                                        {
                                            deleteDescriptionCmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Объект не найден");
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Объект не найден");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void DeleteMaterials() // Удалить ВСЕ материалы
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM materials;";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void DeleteObjects() // Удалить ВСЕ предметы
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM objects;";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void DeleteObjectDescriptions() // Удалить ВСЕ описания
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM object_descriptions;";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Удаление прошло успешно");
        }

        static void Delete() // Удалить ВООБЩЕ ВСЕ
        {
            using (var conn = new NpgsqlConnection(connectingString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        DELETE FROM materials;
                        DELETE FROM objects;
                        DELETE FROM object_descriptions;
                    ";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

/*
 * ТАБЛИЦА 1: materials
 * - id_material: индекс материала   <------------------------------------------
 * - name_material: имя материала                                               |
 *                                                                              |
 *                                                                              |
 * ТАБЛИЦА 2: objects                                                           |
 * - id_object: индекс объекта                                                  |
 * - name_object: имя объекта   <-----------------------------------------------|--------------
 * - materials: материалы (вектор чисел, где каждое число - индекс материала ---               |
 *                                                                                             |
 *                                                                                             |
 * ТАБЛИЦА 3: object_descriptions                                                              |
 * - name_object: имя объекта   ---------------------------------------------------------------
 * - description: описание объекта
 * 
 */