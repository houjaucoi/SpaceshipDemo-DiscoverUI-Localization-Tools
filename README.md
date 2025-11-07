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

7. 在Help/Discover Spaceship Demo Locale, 按"ConvertTo XX", 進行替換文字
<img width="548" height="135" alt="image" src="https://github.com/user-attachments/assets/100c5de6-cc6e-4c8f-9980-39c0c2424cff" />

8. 完成
<img width="632" height="531" alt="image" src="https://github.com/user-attachments/assets/7b12ff27-fada-4207-9e39-772476ae1058" />
<img width="1892" height="525" alt="image" src="https://github.com/user-attachments/assets/8240e00b-fa7d-48f9-b32d-7d7dc14ce4ff" />

# Increase localization

在 DiscoverSpaceshipDemoLocale.cs, *新增*其他語言

1. MenuItem部份
```
[MenuItem("Help/Discover Spaceship Demo Locale/Convert to English", priority = 12)]
static void ToEnglish() => ConvertToLocale("en");

[MenuItem("Help/Discover Spaceship Demo Locale/Convert to English", validate = true)]
static bool ToEnglish_Validate() => CheckDefaultExists();
```

2. Debug部份
```
    private static string NiceName(string id) => id switch
    {
        "zh-cht" => "Traditional Chinese",
        "en" => "English",
        _ => id
    };
```
命名必須以DiscoverDocumentAsset_{localeID}格式修改
<img width="395" height="452" alt="image" src="https://github.com/user-attachments/assets/8d7104ba-4dfb-405d-86a2-cb9c2077bec7" />


