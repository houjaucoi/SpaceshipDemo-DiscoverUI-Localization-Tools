# SpaceshipDemo-DiscoverUI-Localization-Tools
把Unity的SpaceshipDemo範例中Discover UI的"Discover"分頁改成其他文字的工具


# How to use
1. 在Help/Discover Spaceship Demo
<img width="301" height="122" alt="image" src="https://github.com/user-attachments/assets/23d419c1-c354-4406-b76e-67fd7ef243df" />

2. 按"Open Spaceship"
<img width="637" height="550" alt="image" src="https://github.com/user-attachments/assets/c31b0672-33b5-4d87-bf5a-9092389b9caa" />

3.按"Create Default Document Asset",建立Defalut en version asset (*Asset/中沒有DiscoverDocumentAsset_en.asset就不能做下一步*)
<img width="557" height="141" alt="image" src="https://github.com/user-attachments/assets/212026bb-1a94-48f0-8d0b-41844982c095" />

4. 在DiscoverDocumentAsset_en.asset中,按"ExportToJson", 產生english version一份Json file
<img width="718" height="183" alt="image" src="https://github.com/user-attachments/assets/e04583f9-edef-40eb-8434-e99148489708" />

5. 把它給ai生成一份zh-cht version
<img width="1095" height="718" alt="image" src="https://github.com/user-attachments/assets/c1afdadf-f5f4-4747-9667-1c2438d45f2b" />

6. Ctrl+D 複製一份DiscoverDocumentAsset_en.asset改成DiscoverDocumentAsset_zh-cht, 按"ImportFromJson"
<img width="720" height="292" alt="image" src="https://github.com/user-attachments/assets/5b8655e0-2910-41ea-acf3-352792a59bcc" />

7. 完成
<img width="632" height="531" alt="image" src="https://github.com/user-attachments/assets/7b12ff27-fada-4207-9e39-772476ae1058" />
<img width="1892" height="525" alt="image" src="https://github.com/user-attachments/assets/8240e00b-fa7d-48f9-b32d-7d7dc14ce4ff" />
