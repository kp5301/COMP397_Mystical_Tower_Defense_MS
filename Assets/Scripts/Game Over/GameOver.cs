using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    protected LevelManager m_LevelManager;

    /// <summary>
    /// The containing panel of the End Game UI
    /// </summary>
    public Canvas endGameCanvas;

    /// <summary>
    /// Panel that shows final star rating
    /// </summary>
    public ScorePanel scorePanel;

    /// <summary>
    /// Text to be displayed on popup
    /// </summary>
    public string levelCompleteText = "{0} COMPLETE!";

    public string levelFailedText = "{0} FAILED!";

    /// <summary>
    /// Background image
    /// </summary>
    public Image background;

    /// <summary>
    /// Color to set background
    /// </summary>
    public Color winBackgroundColor;

    public Color loseBackgroundColor;

    /// <summary>
    /// Hide the panel if it is active at the start.
    /// Subscribe to the <see cref="LevelManager" /> completed/failed events.
    /// </summary>
    protected void Start()
    {
        if ((m_LevelManager == null) && LevelManager.instanceExists)
        {
            m_LevelManager = LevelManager.instance;
        }
        endGameCanvas.enabled = false;
        //nextLevelButton.enabled = false;
        //nextLevelButton.gameObject.SetActive(false);

        m_LevelManager.levelCompleted += Victory;
        m_LevelManager.levelFailed += Defeat;
    }   
    /// <summary>
    /// Shows the end game screen
    /// </summary>
    protected void OpenEndGameScreen(string endResultText)
    {
        endGameCanvas.enabled = true;

        int score = CalculateFinalScore();
        scorePanel.SetStars(score);


    }

    /// <summary>
    /// Add up the health of all the Home Bases and return a score
    /// </summary>
    /// <returns>Final score</returns>
    protected int CalculateFinalScore()
    {
        return CalculateScore(10, 10);
    }

    /// <summary>
    /// Take the final remaining health of all bases and rates them
    /// </summary>
    /// <param name="remainingHealth">the total remaining health of all home bases</param>
    /// <param name="maxHealth">the total maximum health of all home bases</param>
    /// <returns>0 to 3 depending on how much health is remaining</returns>
    protected int CalculateScore(float remainingHealth, float maxHealth)
    {
        float normalizedHealth = remainingHealth / maxHealth;
        if (Mathf.Approximately(normalizedHealth, 1f))
        {
            return 3;
        }
        if ((normalizedHealth <= 0.9f) && (normalizedHealth >= 0.5f))
        {
            return 2;
        }
        if ((normalizedHealth < 0.5f) && (normalizedHealth > 0f))
        {
            return 1;
        }
        return 0;
    }

    /// <summary>
    /// Occurs when the level is sucessfully completed
    /// </summary>
    protected void Victory()
    {
        OpenEndGameScreen(levelCompleteText);
        background.color = winBackgroundColor;
    }

    /// <summary>
    /// Occurs when level is failed
    /// </summary>
    protected void Defeat()
    {
        OpenEndGameScreen(levelFailedText);
    }
}
