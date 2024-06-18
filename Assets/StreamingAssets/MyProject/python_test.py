import json
from dotenv import load_dotenv
import os

def main():
    load_dotenv()
    video_id = os.getenv('MIIBO_URL')

    return video_id