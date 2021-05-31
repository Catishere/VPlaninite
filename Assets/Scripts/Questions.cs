using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Questions
{
    private static List<Question> _rilaQuestions = new List<Question>()
    {
        new Question("Какво означава “Доункас”?", new List<string> { "връх с реки", "майчина сълза", "ледено езеро", "планина с много вода"}, "планина с много вода"),
        new Question("Какво са Близнака, Рибното, Долното и Трилистника?", new List<string> { "реки", "езера", "хора", "животни"}, "езера"),
        new Question("Как се нарича най-високото езеро в Рила?", new List<string> { "леденото езеро", "сълзата", "диамантеното езеро", "кристалното езеро"}, "леденото езеро"),
        new Question("Каква станция се намира на връх Мусала?", new List<string> { "метеорологична", "пречиствателна", "космическа", "космическа"}, "метеорологична"),
        new Question("Колко е висок връх Мусала?", new List<string> { "2925,4м.", "3194,2м.", "2868,7м.", "2974,5м"}, "2925,4м."),
        new Question("Кой поискал Искър и Места да не се виждат повече?", new List<string> { "майката", "бащата", "Бог", "селяните"}, "майката")
    };

    private static List<Question> _pirinQuestions = new List<Question>()
    {
        new Question("Какво означава “Орбелус”(тракийското име на Пирин)?", new List<string> { "тракийска планина", "космическа планина", "ледена планина", "белоснежна планина"}, "белоснежна планина"),
        new Question("На колко години е Байкушевата мура?", new List<string> { "900г.", "10000г.", "1500г", "1300г."}, "1300г."),
        new Question("От какво е направено по-голямата част от ядрото на Пирин?", new List<string> { "магма", "желязо", "графит", "гранит"}, "гранит"),
        new Question("Как се нарича острата пътека в Пирин?", new List<string> { "Котката", "Кучето", "Орбелус", "Кончето"}, "Кончето"),
        new Question("Как се нарича най-високият връх в Пирин", new List<string> { "Момин двор", "Безбог", "Тодорин връх", "Вихрен"}, "Вихрен"),
        new Question("Кой застрашен вид цвете се намира само в Пирин?", new List<string> { "дяволски орех", "нарцис", "водна лилия", "еделвайс"}, "еделвайс")
    };

    private static List<Question> _vitoshaQuestions = new List<Question>()
    {
        new Question("Кое от изброените имена не е било име на Витоша?", new List<string> { "Скопиос", "Скомиос", "Скромброс", "Хемус"}, "Хемус"),
        new Question("Как се нарича и колко е дълга най-дългата пещера във Витоша?", 
            new List<string> { "Духлата (17600 м)", "Духлата (7600 м)", "Дяволското гърло (17600 м)", "Дяволското гърло (7600 м)"}, "Духлата (17600 м)"),
        new Question("Каква форма има Витоша?", new List<string> { "Кулообразна", "Пирамидална", "Триъгълнообразна", "Конусовидна"}, "Кулообразна"),
        new Question("Как се нарича каменната река във Витоша?", new List<string> {"Морените", "Макароните", "Златните Мостове", "Каменните Мостове"}, "Морените"),
        new Question("Как се нарича първата хижа във Витоша?", new List<string> { "Алеко", "Планинец", "Мотен", "Лале"}, "Алеко"),
        new Question("От легендата за Витоша, кой е овчаря?", new List<string> {"Дружба", "Младост", "Левски Г", "Люлин"}, "Люлин")
    };

    private static List<Question> _rodopiQuestions = new List<Question>()
    {
        new Question("Как се нарича реката, осъществила формирането на Чудните мостове?", new List<string> { "река Арда", "река Марица", "Харманлийска река", "река Еркюприя"}, "река Еркюприя"),
        new Question("Как е наричана още пещера Снежанка?", new List<string> { "Кристалът на Родопите", "Диамантът на Родопите", "Кралицата на Родопите", "Перлата на Родопите"}, "Перлата на Родопите"),
        new Question("През коя епоха е била обитавана Ягодинската пещера?", new List<string> { "бронзовата", "старокаменната", "каменната", "каменно-медната"}, "каменно-медната"),
        new Question("В коя пещера се намира най-високият водопад на Балканите?", new List<string> { "Ухловица", "Снежанка", "Ягодинската пещера", "Дяволското гърло"}, "Дяволското гърло"),
        new Question("Кой е най-високият връх в Родопите?", new List<string> { "Добри връх", "Голям Персенк", "Турлата", "Голям Перелик"}, "Голям Перелик"),
        new Question("Кой бог превърнал Родопа в планина?", new List<string> { "Овидий", "Зевс", "Хера", "Деметра"}, "Овидий")
    };

    private static Dictionary<string, List<Question>> _mountainQuestions = new Dictionary<string, List<Question>>
    {
        {"Рила", _rilaQuestions},
        {"Пирин", _pirinQuestions},
        {"Родопите", _rodopiQuestions},
        {"Витоша", _vitoshaQuestions}
    };

    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static Question GetQuestion(int index)
    {
        var questions = _mountainQuestions[LevelParams.Mountain];
        var question = questions[index];
        question.Answers.Shuffle();
        return question;
    }
}
