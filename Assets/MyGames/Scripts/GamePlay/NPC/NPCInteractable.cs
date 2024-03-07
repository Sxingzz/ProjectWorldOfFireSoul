using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string dialogue = "Xin chào! Tôi là một NPC.";

    public void Interact()
    {
        // Hiển thị hộp thoại văn bản
        dialogueBox.SetActive(true);

        // Cập nhật văn bản bên trong hộp thoại
        dialogueText.SetText(dialogue);
    }

    public void UpdateDialogue()
    {
        // Cập nhật văn bản bên trong hộp thoại
        dialogueText.SetText(dialogue);
    }

    public void OnEnterInteractRange()
    {
        // Hiển thị hộp thoại văn bản
        dialogueBox.SetActive(true);
    }

    public void OnExitInteractRange()
    {
        // Ẩn hộp thoại văn bản
        dialogueBox.SetActive(false);
    }

}


