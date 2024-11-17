using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPOI.HSSF.UserModel.HeaderFooter;

public class IT_DialogueData_CSVReader : MonoSingleton<IT_DialogueData_CSVReader>
{
    public TextAsset csvFile;
    public string mainCSVPath = "Excel/Ch1_01"; 
    public string dialogueFolderPath = "Excel/";

    void Start()
    {
        TestIT(CSVToIT(1));
    }

    public void TestIT(InterrogationTurn interrogationTurn)
    {
        interrogationTurn = CSVToIT(1);

        if (interrogationTurn == null)
        {
            Debug.LogError("IT 為空！");
        }
        else
        {
            Debug.Log(interrogationTurn.ToString());
        }

        if (interrogationTurn.beforeDialogue == null)
        {
            Debug.LogError("IT.beforeDialogue 為空！");
        }
        else
        {
            Debug.Log(interrogationTurn.beforeDialogue.ToString());
        }

        if (interrogationTurn.successDialogue == null)
        {
            Debug.LogError("IT.successDialogue 為空！");
        }
        else
        {
            Debug.Log(interrogationTurn.successDialogue.ToString());
        }

        if (interrogationTurn.failDialogue == null)
        {
            Debug.LogError("IT.failDialogue 為空！");
        }
        else
        {
            Debug.Log(interrogationTurn.failDialogue.ToString());
        }

        if (interrogationTurn.isDMG)
        {
            Debug.Log("IT 為 DMG");
        }
        else
        {
            Debug.Log("IT 為 Non-DMG");
        }

        if(interrogationTurn != null)
        {
            Debug.Log($"IT 物件 ID: {interrogationTurn.GetType().ToString()}");
        }

        if (interrogationTurn.challengeLevel == ChallengeLevel.Easy)
        {
            Debug.Log("挑戰等級為 Easy");
        }
        else if (interrogationTurn.challengeLevel == ChallengeLevel.Normal)
        {
            Debug.Log("挑戰等級為 Normal");
        }
        else if (interrogationTurn.challengeLevel == ChallengeLevel.Hard)
        {
            Debug.Log("挑戰等級為 Hard");
        }
        else if (interrogationTurn.challengeLevel == ChallengeLevel.Fatal)
        {
            Debug.Log("挑戰等級為 Fatal");
        }
    }


    /// <summary>
    /// 根據CSV檔產生三個對話資料，再把對話資料放進InterrogationTurn裡面
    /// </summary>
    /// <param name="_partID"></param>
    /// <param name="turn"></param>
    public InterrogationTurn CSVToIT(int turnID)
    {
        // 建立三個 DialogueData 對象分別對應 before, success 和 fail 段落
        DialogueData beforeDialogue = new DialogueData();
        DialogueData successDialogue = new DialogueData();
        DialogueData failDialogue = new DialogueData();

        if (csvFile == null)
        {
            Debug.LogError("CSV 檔案為空！");
            return null;
        }

        string[] rows = csvFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        bool isDMG = false;
        string challengeLevel = null;
        string ITType = null;
        string dialogueExcelName = null;
        int objectID = 0;

        for (int i = 1; i < rows.Length; i++)
        {
            string[] fields = rows[i].Split(',');

            if (fields.Length < 6)
            {
                Debug.LogError($"CSV 行格式錯誤，缺少欄位：{rows[i]}");
                continue;
            }

            // 根據 turnID 匹配目標行
            if (int.Parse(fields[0]) == turnID)
            {
                isDMG = fields[1].Trim() == "1";
                challengeLevel = fields[2].Trim();
                ITType = fields[3].Trim();
                dialogueExcelName = $"{fields[4].Trim()}";
                challengeLevel = fields[2].Trim();
                objectID = int.Parse(fields[5].Trim());
                break;
            }
        }


        if (string.IsNullOrEmpty(dialogueExcelName))
        {
            Debug.LogError($"未找到對應的 turnID: {turnID}");
            return null;
        }


        string dialogueFilePath = $"Excel/DialogueIT/{dialogueExcelName}";
        TextAsset dialogueFile = Resources.Load<TextAsset>(dialogueFilePath);

        if (dialogueFile == null)
        {
            Debug.LogError($"無法找到對話檔案：{dialogueFilePath}");
            return null;
        }

        string[] sections = dialogueFile.text.Split(new[] { '#' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (sections.Length < 3)
        {
            Debug.LogError("對話檔案格式錯誤，應包含 Before, Success, Fail 三部分！");
            return null;
        }

        beforeDialogue = DialogueDataParse(sections[0]);
        successDialogue = DialogueDataParse(sections[1]);
        failDialogue = DialogueDataParse(sections[2]);

        InterrogationTurn interrogationTurn = null;
        if (ITType == "ObjectSlelect")
        {

            ObjectSlelectIT objectSlelectIT = new ObjectSlelectIT();
            objectSlelectIT.objectID = objectID;
            interrogationTurn = objectSlelectIT;
        }
        else if (ITType == "Option")
        {
            OptionIT optionIT = new OptionIT();
        }
        else if (ITType == "Spotting")
        {
            SpottingIT spottingIT = new SpottingIT();
        }
        else
        {
            Debug.LogError($"未知的 ITType: {ITType}");
            return null;
        }

        switch(challengeLevel)
        {
            case "Easy":
                interrogationTurn.challengeLevel = ChallengeLevel.Easy;
                break;
            case "Normal":
                interrogationTurn.challengeLevel = ChallengeLevel.Normal;
                break;
            case "Hard":
                interrogationTurn.challengeLevel = ChallengeLevel.Hard;
                break;
            case "Fatal":
                interrogationTurn.challengeLevel = ChallengeLevel.Hard;
                break;
            default:
                Debug.LogError($"未知的挑戰等級: {challengeLevel}");
                break;
        }

        interrogationTurn.beforeDialogue = beforeDialogue;
        interrogationTurn.successDialogue = successDialogue;
        interrogationTurn.failDialogue = failDialogue;

        interrogationTurn.isDMG = isDMG;

        return interrogationTurn;

    }

    private DialogueData DialogueDataParse(string section)
    {
        DialogueData dialogueData = new DialogueData();
        List<Speech> speechList = new List<Speech>();

        string[] paragraphs = section.Split(new[] { '*' }, System.StringSplitOptions.RemoveEmptyEntries);

        Debug.Log(paragraphs.Length);

        for (int i = 1; i < paragraphs.Length; i++)//跳過第一行
        {

            Speech speech = ParseLines(paragraphs[i]);
            if (speech != null)
            {
                speechList.Add(speech);
            }
        }
        dialogueData.speechList = speechList;
        return dialogueData;
    }

    private Speech ParseLines(string paragraph)
    {
        List<Sentence> sentences = new List<Sentence>();
        string currentSpeaker = null;

        string[] lines = paragraph.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            if (data.Length != 2)
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
