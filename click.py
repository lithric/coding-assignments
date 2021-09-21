
import pyautogui
import time
import keyboard

# must have code acadamy as 3rd tab and full screen
# open terminal and run "py -m pip install pyautogui time keyboard"

def fuck_code_accadamy():
    keyboard.press_and_release('ctrl+enter')
    pyautogui.click(1775, 945)
    keyboard.press_and_release('ctrl+3')

    time.sleep(2)
    pyautogui.click(1829, 988)
    
    pyautogui.click(1255, 175)
    pyautogui.click(970, 715)


    i = 250
    while i <= 950:

        pyautogui.click(500, i)
        i+=31

    fuck_code_accadamy()

fuck_code_accadamy()
