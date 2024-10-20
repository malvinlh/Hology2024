// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LevelManager : MonoBehaviour
// {
//     public CardFlip[] cardFlips;  // Referensi ke semua kartu di CardSelect

//     private void Start()
//     {
//         // Cek status setiap level
//         for (int i = 0; i < cardFlips.Length; i++)
//         {
//             if (IsLevelCompleted(i + 1))  // Cek apakah level sudah selesai
//             {
//                 cardFlips[i].FlipCardInstant();  // Flip kartu secara instan jika sudah selesai
//             }
//         }
//     }

//     // Cek apakah level sudah selesai menggunakan PlayerPrefs
//     public bool IsLevelCompleted(int levelIndex)
//     {
//         return PlayerPrefs.GetInt("LevelCompleted" + levelIndex, 0) == 1;
//     }

//     // Set level selesai menggunakan PlayerPrefs
//     public void SetLevelCompleted(int levelIndex)
//     {
//         PlayerPrefs.SetInt("LevelCompleted" + levelIndex, 1);
//     }

//     // Dipanggil saat pemain menyelesaikan level
//     public void CompleteLevel(int levelIndex)
//     {
//         SetLevelCompleted(levelIndex);  // Simpan status level
//         UnityEngine.SceneManagement.SceneManager.LoadScene("CardSelect");  // Kembali ke scene CardSelect
//     }

//     // Dipanggil ketika kembali ke scene CardSelect untuk flip card
//     public void OnReturnToCardSelect(int levelIndex)
//     {
//         if (!cardFlips[levelIndex].IsFlipped())
//         {
//             StartCoroutine(cardFlips[levelIndex].FlipCard());  // Flip dengan animasi
//         }
//     }
// }
