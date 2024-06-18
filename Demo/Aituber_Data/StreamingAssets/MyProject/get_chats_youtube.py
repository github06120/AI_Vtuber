from dotenv import load_dotenv
import os
import requests
import json

def get_live_chat_id(youtube_video_id, youtube_data_api_key):
    params = {
        'part' : 'liveStreamingDetails',
        'id' : youtube_video_id,
        'key' : youtube_data_api_key
    }
    response = requests.get(
        'https://youtube.googleapis.com/youtube/v3/videos', params=params
    )
    json_data = response.json()
    
    live_chat_id = json_data['items'][0]['liveStreamingDetails']['activeLiveChatId']
    return live_chat_id

def get_live_chat_messages(live_chat_id, api_key):
    params = {
        'liveChatId': live_chat_id,
        'part': 'id,snippet,authorDetails',
        'maxResults': 200,
        'key': api_key
    }
    response = requests.get(
        'https://youtube.googleapis.com/youtube/v3/liveChat/messages', params=params)
    return response.json()

def main():
    load_dotenv()
    video_id = os.getenv('YOUTUBE_VIDEO_ID')
    api_key = os.getenv('YOUTUBE_DATA_API_KEY')

    live_chat_id = get_live_chat_id(video_id, api_key)

    live_chat_messages = get_live_chat_messages(live_chat_id, api_key)

    messages_path = R'D:\Program Files\UnityProjects\Aituber\Assets\StreamingAssets\MyProject\LiveMessage\youtubelive_chat_messages.json'

    with open(messages_path , 'w') as f:
        pass

    list_dict = []

    for i, message in enumerate(live_chat_messages.get('items', [])):
        chats_dict = {
            'number' : i,
            'author' : message['authorDetails']['displayName'],
            'message' : message['snippet']['displayMessage'],
            'message_type' : message['snippet']['type']
        }
        list_dict.append(chats_dict)

    with open(messages_path, 'a') as f:
        json.dump(list_dict, f, indent=4)