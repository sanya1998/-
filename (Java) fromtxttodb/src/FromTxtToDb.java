import java.io.*;
import java.nio.charset.Charset;
import java.util.ArrayList;

public class FromTxtToDb {
    public static void main(String[] args) {
        String path = "DateBase_Books";
        File folder_main = new File(path);

        ArrayList<Book> books = new ArrayList<>();

        // По содержимому папки folder_main (DateBase_Books):
        for (File folder_genre : folder_main.listFiles()) {
            if(folder_genre.isDirectory()) // Папка "Жанр_i"
            {
                String category_name =  folder_genre.getName();
                // 1) Добавить эту категорию в БД (или проверять нет ли ее уже в БД).
                // 2) Получить ID этой категории, чтобы потом добавить его к book.categoryId

                // По содержимому папки "Жанр_i":
                for(File file_book : folder_genre.listFiles()){
                    if(file_book.isDirectory()) { // Папка "Книга_i"
                        Book book = new Book();
                        // book.categoryId = ID этой категории;
                        book.rating = (int) (Math.random() * 100);

                        // По содержимому папки "Книга_i":
                        for(File file : file_book.listFiles())
                        {
                            if(file.getName().equals("info.txt"))
                            {
                                // Считываем всю инфу про книгу
                                try{
                                    InputStream input = new FileInputStream(file);
                                    BufferedReader reader = new BufferedReader(new InputStreamReader(input, Charset.forName("Unicode")));
                                    String line;
                                    while((line = reader.readLine()) != null)
                                    {
                                        if(line.contains("Название"))
                                            book.name = line.replace("Название : ", "");
                                        else if(line.contains("Цена"))
                                            book.price = Double.parseDouble(line
                                                    .replace("Цена : ", "")
                                                    .replace(" руб.", ""));
                                        else if(line.contains("ФИО автора"))
                                            book.author = line.replace("ФИО автора : ", "");
                                        else if(line.contains("Количество страниц"))
                                            book.pageCount = Integer.parseInt(line
                                                    .replace("Количество страниц : ", "")
                                                    .replace(" страниц", ""));
                                        else if(line.contains("Книг в наличии")) {
                                            String count_0 = line.replace("Книг в наличии : ", "");
                                            if(!count_0.equals("—"))
                                                book.count = Integer.parseInt(count_0);
                                        }
                                        else if(line.contains("Издательство"))
                                        {
                                            String publisher = line.replace("Издательство : ", "");

                                            // 1) Если этого издательства нет в БД, то внести
                                            // if(false){}

                                            // 2) Узнать ID издательства, чтобы потом добавить его к book.publisherId

                                            // book.publisherId = ID издательства;
                                        }
                                        else if(line.contains("Аннотация"))
                                        { /* Само описание книги начинается со следующей строки*/
                                            if((line = reader.readLine()) != null)
                                                book.description = line;
                                        }

                                    }
                                } catch (Exception e){
                                    System.out.println(e.toString());
                                }
                            }
                            else if(file.getName().equals("picture.jpg"))
                            {
                                // file.toString() - путь до изображения, начиная с папки folder_main
                                book.path_to_picture = file.toString();
                            }
                        }
                        books.add(book); // Добавляем книгу в список
                    }
                }
            }

        }

        /*
        // Выведем 1 из книг
        int num = 5;
        System.out.println(books.get(num).name);
        System.out.println(books.get(num).author);
        System.out.println(books.get(num).price.toString());
        System.out.println(books.get(num).pageCount.toString());
        System.out.println(books.get(num).path_to_picture);
        System.out.println(books.get(num).description);
        System.out.println(books.get(num).count.toString());
        System.out.println(books.get(num).rating.toString());
        */

        // Добавление книг в БД
        /*for (Book book : books)
        {
            // Добавляем каждую книгу в БД
        }*/
    }
    public static class Book
    {

        Long id = 0L; // В этом скрипте, наверно, не пригодится.

        String name = "без названия";
        String author = "нет автора";
        Integer pageCount = 0;

        String path_to_picture = "";

        Long categoryId = 0L;
        Long publisherId = 0L;

        String description = "нет аннотации";

        Double price = 0.0;

        Integer count = 0;
        Integer rating = 0;
    }
}
