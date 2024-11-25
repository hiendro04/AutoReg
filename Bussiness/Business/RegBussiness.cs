﻿using Bussiness.Dao;
using Bussiness.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.IO.Compression;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using static OpenQA.Selenium.BiDi.Modules.Script.RemoteValue;

namespace Bussiness.Business
{
    public class RegBussiness
    {
        public void Reg(ConfigInfo configInfo, PersonalInfo personalInfo)
        {
            // Tự động tải Chromedriver phù hợp
            new DriverManager().SetUpDriver(new ChromeConfig());
            var a = "C:\\Users\\user\\AppData\\Local\\Google\\Chrome\\User Data\\Profile 9";
            // Đường dẫn đến profile của Chrome
            string chromeProfilePath = personalInfo.ProfilePath; // Thay đổi đường dẫn nếu cần
            string chromeProfileName = personalInfo.ProfileName; // Thay đổi đường dẫn nếu cần
            var options = new ChromeOptions();
            //options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument($"--user-data-dir={a}");
            //options.AddArgument($"--user-data-dir={chromeProfilePath}");
            //options.AddArgument($"--profile-directory={chromeProfileName}");

            // Khởi tạo ChromeDriver
            IWebDriver driver = new ChromeDriver(options);
            // Mở trang web
            driver.Url = configInfo.Link;
            driver.Navigate();

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3000));

                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");

                });

                // Lấy chiều cao của trang (scrollHeight) bằng JavaScript
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                var scrollHeight = (long)jsExecutor.ExecuteScript("return document.body.scrollHeight");
                // Tính vị trí cần cuộn (50% chiều cao trang)
                long scrollPosition = scrollHeight / 2;
                // Cuộn xuống 50% trang
                jsExecutor.ExecuteScript($"window.scrollTo(0, {scrollPosition});");

                //buoc 1 - chon
                var btnRegClassname = "btnGruen";
                var button1 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName(btnRegClassname)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button1);
                wait.Until(driver =>
                {
                    return button1.Displayed && button1.Enabled && !IsElementCovered(driver, button1); ;
                });
                if(button1 == null)
                {
                    driver.Quit();
                    Reg(configInfo, personalInfo);
                }
                //button1.Click();
                var actions1 = new Actions(driver);
                actions1.MoveToElement(button1).Click().Perform();

                //buoc 2 - next
                var btnNextClassname = "cs-button--arrow_next";
                var button2 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName(btnNextClassname)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button2);
                wait.Until(driver =>
                {
                    return button2.Displayed && button2.Enabled;
                });


                //reading
                var reading = driver.FindElement(By.XPath("//input[normalize-space(@id)='reading']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" reading \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" reading \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //listening
                var listening = driver.FindElement(By.XPath("//input[@id=' listening ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" listening \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" listening \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //writing
                var writing = driver.FindElement(By.XPath("//input[@id=' writing ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" writing \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" writing \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //speaking
                var speaking = driver.FindElement(By.XPath("//input[@id=' speaking ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" speaking \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" speaking \""]::after { initial : none !important; }';
                            }
                        ");
                }

                button2.Click();

                //buoc 3 - book my selft
                var btnBookMySelftSelector = ".cs-layer__button-wrapper .cs-button.cs-layer__button--high:nth-of-type(2)";
                //IWebElement button3 = driver.FindElement(By.CssSelector(btnBookMySelftSelector));
                var button3 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnBookMySelftSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button3);
                wait.Until(driver =>
                {
                    return button3.Displayed && button3.Enabled;
                });
                button3.Click();

                //buoc 4 - nhap tai khoan
                var inputEmailId = "username";
                //var inputEmail = driver.FindElement(By.Id(inputEmailId));
                var inputEmail = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.Id(inputEmailId)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                wait.Until(driver =>
                {
                    return inputEmail.Displayed && inputEmail.Enabled;
                });
                inputEmail.SendKeys(personalInfo.Username);

                var inputPwId = "password";
                //var inputPw = driver.FindElement(By.Id(inputPwId));
                var inputPw = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.Id(inputPwId)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                wait.Until(driver =>
                {
                    return inputPw.Displayed && inputPw.Enabled;
                });
                inputPw.SendKeys(personalInfo.Password);

                var btnLoginSelector = "#fm1 > input.btn.submit.arrow.right";
                //IWebElement button4 = driver.FindElement(By.CssSelector(btnRegSelector));
                var button4 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnLoginSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button4);
                wait.Until(driver =>
                {
                    return button4.Displayed && button4.Enabled;
                });
                button4.Click();

                //buoc 5 thanh toan - continue
                var btnPaymentContinueSelector = ".cs-checkout__bottom button.cs-button--arrow_next";
                var button5 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnPaymentContinueSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button5);
                wait.Until(driver =>
                {
                    return button5.Displayed && button5.Enabled;
                });
                button5.Click();
                Task.Delay(2000).Wait();
            }
            catch
            {
                driver.Manage().Cookies.DeleteAllCookies();
                //driver.Quit();
                //Reg(configInfo, personalInfo);
                throw;
            }
        }

        // Phương thức kiểm tra xem phần tử có bị che khuất không
        private static bool IsElementCovered(IWebDriver driver, IWebElement element)
        {
            var location = element.Location;
            var size = element.Size;

            // Tính toán tọa độ giữa của phần tử
            int centerX = location.X + size.Width / 2;
            int centerY = location.Y + size.Height / 2;

            // Sử dụng JavaScript để kiểm tra phần tử tại tọa độ đó
            var js = (IJavaScriptExecutor)driver;
            var elementAtPoint = js.ExecuteScript("return document.elementFromPoint(arguments[0], arguments[1]);", centerX, centerY);

            return elementAtPoint != null && !elementAtPoint.Equals(element);
        }

        public IWebDriver OpenNewChrome(ConfigInfo configInfo, PersonalInfo personalInfo, string proxyStr)
        {
            string[] proxyParts = proxyStr.Split(':');
            string proxyIp = proxyParts[0];
            int proxyPort = int.Parse(proxyParts[1]);
            string proxyUser = proxyParts[2];
            string proxyPass = proxyParts[3];

            string extensionPath = CreateProxyAuthExtension(proxyIp, proxyPort, proxyUser, proxyPass);
            // Đường dẫn đến profile của Chrome
            string chromeProfilePath = personalInfo.ProfilePath; // Thay đổi đường dẫn nếu cần
            string chromeProfileName = personalInfo.ProfileName; // Thay đổi đường dẫn nếu cần
            var options = new ChromeOptions();
            //options.AddArgument($"--user-data-dir={chromeProfilePath + "\\" + chromeProfileName}");
            //options.AddArgument($"--proxy-server=http://{proxyIp}:{proxyPort}");
            options.AddExtension(extensionPath);
            options.AddArgument($"--user-data-dir={chromeProfilePath}");
            options.AddArgument($"--profile-directory={chromeProfileName}");

            // Khởi tạo ChromeDriver
            IWebDriver driver = new ChromeDriver(options);
            // Mở trang web
            driver.Url = configInfo.Link;
            driver.Navigate();

            return driver;
        }

        public void RegAction(IWebDriver driver, ConfigInfo configInfo, PersonalInfo personalInfo)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.SendKeys(Keys.F5).Perform();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3000));

                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");

                });

                // Lấy chiều cao của trang (scrollHeight) bằng JavaScript
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                var scrollHeight = (long)jsExecutor.ExecuteScript("return document.body.scrollHeight");
                // Tính vị trí cần cuộn (50% chiều cao trang)
                long scrollPosition = scrollHeight / 2;
                // Cuộn xuống 50% trang
                jsExecutor.ExecuteScript($"window.scrollTo(0, {scrollPosition});");

                //buoc 1 - chon
                var btnRegClassname = "btnGruen";
                var button1 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName(btnRegClassname)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button1);
                wait.Until(driver =>
                {
                    return button1.Displayed && button1.Enabled && !IsElementCovered(driver, button1); ;
                });
                if (button1 == null)
                {
                    driver.Quit();
                    Reg(configInfo, personalInfo);
                }
                //button1.Click();
                actions.MoveToElement(button1).Click().Perform();

                //buoc 2 - next
                var btnNextClassname = "cs-button--arrow_next";
                var button2 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName(btnNextClassname)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button2);
                wait.Until(driver =>
                {
                    return button2.Displayed && button2.Enabled;
                });


                //reading
                var reading = driver.FindElement(By.XPath("//input[normalize-space(@id)='reading']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" reading \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" reading \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //listening
                var listening = driver.FindElement(By.XPath("//input[@id=' listening ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" listening \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" listening \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //writing
                var writing = driver.FindElement(By.XPath("//input[@id=' writing ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" writing \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" writing \""]::after { initial : none !important; }';
                            }
                        ");
                }

                //speaking
                var speaking = driver.FindElement(By.XPath("//input[@id=' speaking ']"));
                //bo chon
                if (!personalInfo.IsReading && reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" speaking \""]::after { content: none !important; }';
                            }
                        ");
                }
                //chon
                if (personalInfo.IsReading && !reading.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            var style = document.querySelector('style');
                            if (style) {
                                style.innerHTML = 'label[for=\"" speaking \""]::after { initial : none !important; }';
                            }
                        ");
                }

                button2.Click();

                //buoc 3 - book my selft
                var btnBookMySelftSelector = ".cs-layer__button-wrapper .cs-button.cs-layer__button--high:nth-of-type(2)";
                //IWebElement button3 = driver.FindElement(By.CssSelector(btnBookMySelftSelector));
                var button3 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnBookMySelftSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button3);
                wait.Until(driver =>
                {
                    return button3.Displayed && button3.Enabled;
                });
                button3.Click();

                //buoc 4 - nhap tai khoan
                var inputEmailId = "username";
                //var inputEmail = driver.FindElement(By.Id(inputEmailId));
                var inputEmail = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.Id(inputEmailId)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                wait.Until(driver =>
                {
                    return inputEmail.Displayed && inputEmail.Enabled;
                });
                inputEmail.SendKeys(personalInfo.Username);

                var inputPwId = "password";
                //var inputPw = driver.FindElement(By.Id(inputPwId));
                var inputPw = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.Id(inputPwId)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                wait.Until(driver =>
                {
                    return inputPw.Displayed && inputPw.Enabled;
                });
                inputPw.SendKeys(personalInfo.Password);

                var btnLoginSelector = "#fm1 > input.btn.submit.arrow.right";
                //IWebElement button4 = driver.FindElement(By.CssSelector(btnRegSelector));
                var button4 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnLoginSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button4);
                wait.Until(driver =>
                {
                    return button4.Displayed && button4.Enabled;
                });
                button4.Click();

                //buoc 5 thanh toan - continue
                var btnPaymentContinueSelector = ".cs-checkout__bottom button.cs-button--arrow_next";
                var button5 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.CssSelector(btnPaymentContinueSelector)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button5);
                wait.Until(driver =>
                {
                    return button5.Displayed && button5.Enabled;
                });
                button5.Click();
            }
            catch
            {
                driver.Manage().Cookies.DeleteAllCookies();
                //driver.Quit();
                //Reg(configInfo, personalInfo);
                throw;
            }
        }

        // Tạo extension để tự động nhập thông tin đăng nhập vào proxy
        static string CreateProxyAuthExtension(string proxyHost, int proxyPort, string proxyUser, string proxyPass)
        {
            string extensionFolder = Path.Combine(Path.GetTempPath(), "ProxyAuthExtension_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(extensionFolder);

            string manifestJson = @"
                {
                    ""version"": ""1.0.0"",
                    ""manifest_version"": 2,
                    ""name"": ""Chrome Proxy"",
                    ""permissions"": [ ""proxy"", ""tabs"", ""unlimitedStorage"", ""storage"", ""<all_urls>"", ""webRequest"", ""webRequestBlocking"" ],
                    ""background"": { ""scripts"": [ ""background.js"" ] },
                    ""minimum_chrome_version"": ""22.0.0""
                }";

            string backgroundJs = $@"
                var config = {{
                    mode: ""fixed_servers"",
                    rules: {{
                      singleProxy: {{
                        scheme: ""http"",
                        host: ""{proxyHost}"",
                        port: {proxyPort}
                      }},
                      bypassList: [""localhost""]
                    }}
                }};

                chrome.proxy.settings.set({{value: config, scope: ""regular""}}, function() {{}});

                chrome.webRequest.onAuthRequired.addListener(
                    function(details, callback) {{
                        callback({{authCredentials: {{username: ""{proxyUser}"", password: ""{proxyPass}""}}}});
                    }},
                    {{urls: [""<all_urls>""]}},
                    [""blocking""]
                );
                ";

            File.WriteAllText(Path.Combine(extensionFolder, "manifest.json"), manifestJson);
            File.WriteAllText(Path.Combine(extensionFolder, "background.js"), backgroundJs);

            string zipPath = extensionFolder + ".zip";
            ZipFile.CreateFromDirectory(extensionFolder, zipPath);

            return zipPath;
        }

        public bool CheckActive(ConfigInfo configInfo)
        {
            string chromeProfilePath = "D:\\ChromeProfile"; // Thay đổi đường dẫn nếu cần
            string chromeProfileName = "Default"; // Thay đổi đường dẫn nếu cần
            var options = new ChromeOptions();
            //options.AddArgument($"--user-data-dir={chromeProfilePath + "\\" + chromeProfileName}");
            options.AddArgument($"--user-data-dir={chromeProfilePath}");
            options.AddArgument($"--profile-directory={chromeProfileName}");

            // Khởi tạo ChromeDriver
            IWebDriver driver = new ChromeDriver(options);
            // Mở trang web
            driver.Url = configInfo.Link;
            driver.Navigate();

            var check = false;

            while (!check)
            {
                Actions actions = new Actions(driver);
                actions.SendKeys(Keys.F5).Perform();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3000));

                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");

                });

                // Lấy chiều cao của trang (scrollHeight) bằng JavaScript
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                var scrollHeight = (long)jsExecutor.ExecuteScript("return document.body.scrollHeight");
                // Tính vị trí cần cuộn (50% chiều cao trang)
                long scrollPosition = scrollHeight / 2;
                // Cuộn xuống 50% trang
                jsExecutor.ExecuteScript($"window.scrollTo(0, {scrollPosition});");

                //buoc 1 - chon
                var btnRegClassname = "btnGruen";
                var button1 = wait.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName(btnRegClassname)); // Thay thế bằng ID thực tế
                    return (element.Displayed && element.Enabled) ? element : null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button1);
                wait.Until(driver =>
                {
                    return button1.Displayed && button1.Enabled && !IsElementCovered(driver, button1); ;
                });
                if (button1 != null)
                {
                    check = true;
                }
            }

            return check;
        }
    }
}
