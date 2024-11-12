using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPOI.HSSF.UserModel.HeaderFooter;

public class IT_DialogueData_CSVReader : MonoSingleton<IT_DialogueData_CSVReader>
{
    public TextAsset csvFile;

    void Start()
    {
        InterrogationTurn dialogueTurn = new ObjectSlelectIT();
        CSVToIT(dialogueTurn);
        TestDialogueData(dialogueTurn.beforeDialogue);
        TestDialogueData(dialogueTurn.successDialogue);
        TestDialogueData(dialogueTurn.failDialogue);
    }

    /// <summary>
    /// 根據CSV檔產生三個對話資料，再把對話資料放進InterrogationTurn裡面
    /// </summary>
    /// <param name="_partID"></param>
    /// <param name="turn"></param>
    public void CSVToIT(InterrogationTurn turn)
    {
        // 建立三個 DialogueData 對象分別對應 before, success 和 fail 段落
        DialogueData beforeDialogue = new DialogueData();
        DialogueData successDialogue = new DialogueData();
        DialogueData failDialogue = new DialogueData();

        if (csvFile == null)
        {
            Debug.LogError("CSV 檔案為空！");
            return;
        }

        
        string[] sections = csvFile.text.Split(new[] { '#' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (sections.Length < 3)
        {
            Debug.LogError("CSV 檔案格式錯誤，缺少對話部分！");
            return;
        }

        List<Speech> beforeSpeechList = ParseSection(sections[0]);
        List<Speech> successSpeechList = ParseSection(sections[1]);
        List<Speech> failSpeechList = ParseSection(sections[2]);

        beforeDialogue.speechList = beforeSpeechList;
        successDialogue.speechList = successSpeechList;
        failDialogue.speechList = failSpeechList;

        turn.AddDialogueData(beforeDialogue, successDialogue, failDialogue);
    }

    private List<Speech> ParseSection(string section)
    {
        List<Speech> speechList = new List<Speech>();

        string[] paragraphs = section.Split(new[] { '*' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 1; i < paragraphs.Length; i++)//跳過第一行
        {

            Speech speech = ParseLines(paragraphs[i]);
            if (speech != null)
            {
                speechList.Add(speech);
            }
        }
        return speechList;
    }

    private Speech ParseLines(string paragraph)
    {
        List<Sentence> sentences = new List<Sentence>();
        string currentSpeaker = null;

        string[] lines = paragraph.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            if (data.Length < 2)
            {
                Debug.LogError($"行格式錯誤，缺少欄位: {line}");
                continue;
            }

            string speaker = data[0].Trim();
            string contentLine = data[1].Trim();
            if (!string.IsNullOrEmpty(speaker))
            {
                currentSpeaker = speaker;
            }
            else
            {
                speaker = currentSpeaker;
            }
            Sentence sentence = CreateSentence(contentLine);
            sentences.Add(sentence);
        }
        return CreateSpeech(currentSpeaker, sentences);
    }


    private Speech CreateSpeech(string _speaker, List<Sentence> _content)
    {
        Character character = CharacterDataBaseSystem.instance.NameToCharacter(_speaker);

        return new Speech(character, _content);
    }

    private Sentence CreateSentence(string _line)
    {
        return new Sentence(_line);
    }

    private DialogueData CreateDialogueData(List<Speech> _speechList)
    {
        return new DialogueData(_speechList);
    }

    public void TestDialogueData(DialogueData dialogueData)
    {

        if (dialogueData == null || dialogueData.speechList == null || dialogueData.speechList.Count == 0)
        {
            Debug.LogError("對話資料為空！");
            return;
        }

        foreach (Speech speech in dialogueData.speechList)
        {
            Debug.Log(speech.speaker.characterName ?? "無對話人物");

            foreach (Sentence sentence in speech.content)
            {
                Debug.Log(sentence.line);
            }
        }
    }
}
