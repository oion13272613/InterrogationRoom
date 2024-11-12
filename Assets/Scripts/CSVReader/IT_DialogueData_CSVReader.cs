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
    /// �ھ�CSV�ɲ��ͤT�ӹ�ܸ�ơA�A���ܸ�Ʃ�iInterrogationTurn�̭�
    /// </summary>
    /// <param name="_partID"></param>
    /// <param name="turn"></param>
    public void CSVToIT(InterrogationTurn turn)
    {
        // �إߤT�� DialogueData ��H���O���� before, success �M fail �q��
        DialogueData beforeDialogue = new DialogueData();
        DialogueData successDialogue = new DialogueData();
        DialogueData failDialogue = new DialogueData();

        if (csvFile == null)
        {
            Debug.LogError("CSV �ɮ׬��šI");
            return;
        }

        
        string[] sections = csvFile.text.Split(new[] { '#' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (sections.Length < 3)
        {
            Debug.LogError("CSV �ɮ׮榡���~�A�ʤֹ�ܳ����I");
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
        for (int i = 1; i < paragraphs.Length; i++)//���L�Ĥ@��
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
                Debug.LogError($"��榡���~�A�ʤ����: {line}");
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
            Debug.LogError("��ܸ�Ƭ��šI");
            return;
        }

        foreach (Speech speech in dialogueData.speechList)
        {
            Debug.Log(speech.speaker.characterName ?? "�L��ܤH��");

            foreach (Sentence sentence in speech.content)
            {
                Debug.Log(sentence.line);
            }
        }
    }
}
