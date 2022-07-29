# KnutAlgorCSharpSols (English)
Realization of sort and search algorithms described in Chapter 6 of Donald Knut book "The Art of Computer Programming", Volume 3 (by C# language means). 

-	Sequential searching in ordered table (marked as Algorithm T in the Section 6.1)
-	Tree search and insertion (6.2.2T)
-	Tree deletion (6.2.2D)
-	Balanced tree search and insertion (6.2.3A)
-	Trie search (6.3T)
-	Digital tree search and insertion (6.3D)

### Unordered or ordered table of records

The record of unordered or ordered table is reflected by the struct type with two fields – integer number *key* which is used for identification – and real number (floating-point number with double precision, *double* type) *exp_value* that is the record query probability. Values of the concrete instance are initialized by the constructor call; all fields are available only for reading through the properties *Key* и *Probability* respectively.

The three records’ lists in which query probability values follow different distributions (geometric with *p=q=0.5*, binomial with *p=q=0.5* and wedge-shaped ones) were firstly generated and written (serialized) by supporting program (the *SearchandSort1* solution, the *Task1_1* project) into binary files for reading and usage after ordering through the Cocktail shaker sort execution by the main one (access control program, the *Task1* project).

### Tape/self-organizing file

The record at the tape or in self-organizing file was initially reflected by the struct type with two fields – identification integer number *key* which is used for sorting – and symbolic string *val* that is the value of the record. Then there were two fields added for reading and searching operation execution – fields *position* and *byte_length* which contains the position in file and the byte length of the record respectively; all fields are available only for reading through the properties. All this values was firstly generated and written by supporting programs (the *SearchandSort1* solution, the *Task2* and *Task2_1* project) into the text file – links’ file – for reading and usage by the main one (the *SearchandSort1* solution, the *Task3_1* project).

Tree search and insertion algorithms (in binary, *n*-ary and balanced trees) with deletion ones were applied to links’ files construction and redaction (the *SearchandSort2*, *SearchandSort3* and *SearchandSort4* solutions).

# KnutAlgorCSharpSols (Русский)
Реализация алгоритмов поиска и сортировки, описанных в главе 6, том 3 книги Д. Кнута "Искусство программирования", средствами языка С#. 

-	Последовательный поиск в упорядоченной таблице (обозначен как алгоритм T в разделе 6.1)
-	Поиск по дереву со вставкой (6.2.2T)
-	Удаление из дерева (6.2.2D)
-	Поиск со вставкой по сбалансированному дереву (6.2.3A)
-	Лучевой поиск (6.3T)
-	Цифровой поиск со вставкой по дереву (6.3D)

### Таблицы записей (с разными распределениями вероятности запроса)

Запись (*record*) в неупорядоченной или упорядоченной таблице/массиве представлена структурой (тип *struct*) с двумя полями – идентификационным целочисленным ключом *key* и вещественным числом (числом с плавающей точкой двойной точности, тип *double*) *exp_value*, представляющим собой вероятность запроса записи. Значения в конкретном экземпляре задаются через конструктор структуры; поля доступны только для чтения через свойства *Key* и *Probability* соответственно.

Для проверки корректности реализации алгоритма были сгенерированы и записаны (сериализованы) вспомогательной программой (проект *Task1_1*) в бинарные файлы три списка (*list*) записей, вероятности запроса в которых подчиняется одному из распределений (геометрическое с *p=q=0.5*, биномиальное с *p=q=0.5* и клиновидное). Перед выполнением поиска/вставки программой управления доступом (проект *Task1*) проводится упорядочивание скопированного из файла списка посредством выполнения шейкер-сортировки по значению вероятности запроса.

### Лента с неравными записями / самоорганизующийся файл

Запись (*record*) на ленте или в самоорганизующемся файле изначально была представлена структурой (тип *struct*) с двумя полями – целочисленным ключом *key*, являющийся идентификатором и по значению которого и проводится сортировка, и символьной строкой *val*, являющейся значением записи. Потом для облегчения процедур чтения и поиска блоков были добавлены поля *position* и *byte_length* для хранения и применения значений позиции в файле и длины в битах для каждой записи соответственно; все поля доступны только для чтения через свойства. Все сведения о записях изначально составлялись и записывались посредством вспомогательных программ (проекты *Task2* и *Task2_1*) в текстовый файл – файл ссылок – для выполнения последующих операций.

Алгоритмы поиска со вставкой по дереву (бинарному, *n*-арному и сбалансированному) и удаления из дерева были применены к построению, чтению и редактированию файла ссылок (*SearchandSort2*, *SearchandSort3* и *SearchandSort4*).
