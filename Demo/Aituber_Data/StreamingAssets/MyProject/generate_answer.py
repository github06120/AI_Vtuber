from dotenv import load_dotenv
import os
import requests

def generate(input_comment):
    load_dotenv()
    MIIBO_URL = os.getenv('MIBBO_URL')

    headers = {'content-type': 'application/json'}
    item_data = {
        'api_key': '68fef931-4b47-4ff3-b384-35f5befd229f18ecb7409ad197',
        'agent_id': 'b9903c07-7c18-422e-ac5f-09f8d6ba9f2818ecb731ff83d1',
        'utterance': input_comment
    }

    
    r = requests.post(
        "https://api-mebo.dev/api",
        json = item_data,
        headers = headers
    )

    res = r.json()['bestResponse']['utterance']
    return res