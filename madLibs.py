from random import randint
madLibs = [
    lambda x: f"My name is {input('enter a name')}, you might be wondering how {input('enter an event')}... well... it's a long story...",
    lambda x: f"I work at {input('enter a occupation')}, so my life is {input('enter an adjective')}... this is totally fine for several reasons...",
    lambda x: f"I was in the {input('enter a place')} when an {input('enter an event')} suddenly made me {input('enter an action')}, it was too much to handle..."
]
print(madLibs[randint(0,2)](0))