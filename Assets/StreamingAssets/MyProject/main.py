import get_chats_youtube
import generate_answer
import json


comment_count_ref = 0
super_chat_user = []
super_chat_comment = []

def Main():
    get_chats_youtube.main()

    youtube_comment_path = R'D:\Program Files\UnityProjects\Aituber\Assets\StreamingAssets\MyProject\LiveMessage\youtubelive_chat_messages.json'
    with open(youtube_comment_path) as f:
        d = json.load(f)

    global comment_count_ref
    if len(d) > comment_count_ref:

        for i in range(comment_count_ref, len(d)):
            if d[i]['message_type'] == "superChatEvent":
                super_chat_user.append(d['author'])
                super_chat_comment.append(d['message'])

        ###スパチャ優先###
        if len(super_chat_user) > 0 :
            answer = generate_answer.generate(super_chat_comment[0])
            answer = super_chat_user[0] + "さんスパチャありがとう!!" + super_chat_comment[0] + answer
            del super_chat_user[0]
            del super_chat_comment[0]
            comment_count_ref = len(d)
            return answer
        ###スパチャない時最新###
        else :
            message = d[len(d) - 1]['message']
            answer = generate_answer.generate(message)
            comment_count_ref = len(d)
            return message
    
    return None