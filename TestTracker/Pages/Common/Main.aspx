<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TestTracker.Pages.Common.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Styles -->
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,400;0,600;0,700;1,400&display=swap" rel="stylesheet" />
    <link href="../../Content/css/landing.css" rel="stylesheet" />
    <link rel="icon" href="../../Content/img/logo.svg" type="image/x-icon" />
    <link rel="stylesheet" href="../../Content/bootstrap-grid.min.css" />
    <link rel="stylesheet" href="../../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../../Content/css/landing.css" />
    <link rel="stylesheet" href="../../Content/fontawesome/css/all.min.css" />
    <script type="text/javascript" src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" src="../../Scripts/popper.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg fixed-top navbar-dark" style="background: #13b987; padding-top: 10px">
            <div class="container">
                <a class="navbar-brand logo-image" href="Main.aspx" style="text-decoration: none">
                    <img src="../../Content/img/logo.svg" alt="alternative" style="width: 40px; height: 40px" />
                    Test tracker</a>

                <button class="navbar-toggler p-0 border-0" type="button" data-toggle="offcanvas">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="navbar-collapse offcanvas-collapse" id="navbarsExampleDefault">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link page-scroll" href="#header">Главная <span class="sr-only">(current)</span></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link page-scroll" href="#features">Возможности</a>
                        </li>
                    </ul>
                    <span class="nav-item">
                        <asp:Button runat="server" ID="btEnter" class="btn-outline-sm" OnClick="btEnter_Click"/>
                    </span>
                </div>
            </div>
        </nav>
        <header id="header" class="header">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="text-container">
                            <h1 class="h2-large">Система контроля исполнения ручного тестирования</h1>
                            <p class="p-large">Используйте test tracker, чтобы упростить и ускорить процесс контроля тестирования ваших программных продуктов</p>
                            <a class="btn-solid-lg page-scroll" href="#description">Подробнее</a>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="image-container">
                            <img class="img-fluid rounded" src="../../Content/img/Main2.png" alt="alternative" />
                        </div>
                    </div>
                </div>
            </div>
            <svg class="header-decoration" data-name="Layer 3" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="none" viewBox="0 0 1920 305.139">
                <defs>
                    <style>
                        .cls-1 {
                            fill: #ffffff;
                        }
                    </style>
                </defs><title>header-decoration</title><path class="cls-1" d="M1486.486,36.773C1434.531,13.658,1401.068-5.1,1329.052,1.251c-92.939,8.2-152.759,70.189-180.71,89.478-23.154,15.979-134.286,104.091-171.753,128.16-50.559,32.48-98.365,59.228-166.492,67.5-67.648,8.21-124.574-6.25-152.992-18-42.218-17.454-42.218-17.454-90-39-35.411-15.967-81.61-34.073-141.58-34.054-116.6.037-262.78,77.981-354.895,80.062C53.1,275.793,22.75,273.566,0,260.566v44.573H1920V61.316c-36.724,23.238-76.008,61.68-177,65C1655.415,129.2,1556.216,67.8,1486.486,36.773Z" transform="translate(0 0)" /></svg>
        </header>
        <div id="description" class="cards-1" style="padding-top: 6rem">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-title">ОПИСАНИЕ</div>
                        <h2 class="h2-heading">Автоматизация контроля тестирования сэкономит ваше время и силы на разработку</h2>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-image">
                                <img class="img-fluid" src="../../Content/img/description-icon-1.svg" alt="alternative">
                            </div>
                            <div class="card-body">
                                <h5 class="card-title">Создание тестов</h5>
                                <p>Начать создавать и описывать тесты для вашего проекта очень просто и удобно</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-image">
                                <img class="img-fluid" src="../../Content/img/description-icon-2.svg" alt="alternative" />
                            </div>
                            <div class="card-body">
                                <h5 class="card-title">Экспорт отчётов</h5>
                                <p>Забудьте о работе в старых электронных таблицах. Для создания отчёта достаточно нажать одну кнопку</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-image">
                                <img class="img-fluid" src="../../Content/img/description-icon-3.svg" alt="alternative">
                            </div>
                            <div class="card-body">
                                <h5 class="card-title">Автоматизация</h5>
                                <p>Test tracker позволит избежать рутинной и надоедливой мелкой работы</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="features" class="tabs bg-gray">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="section-title">ВОЗМОЖНОСТИ</div>
                        <h2 class="h2-heading">Автоматизация контроля тестирования</h2>
                        <p class="p-heading">Выведите процесс тестирования на новый уровень и автоматизируйте контроля исполнения тестирования, чтобы сэкономить время на разработку продукта</p>
                    </div>
                </div>
                <div class="row">
                    <ul class="nav nav-tabs" id="templateTabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="nav-tab-1" data-toggle="tab" href="#tab-1" role="tab" aria-controls="tab-1" aria-selected="true"><i class="fas fa-check-circle"></i>Создание тестов</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="nav-tab-2" data-toggle="tab" href="#tab-2" role="tab" aria-controls="tab-2" aria-selected="false"><i class="fas fa-file-excel"></i>Экспорт отчётов</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="nav-tab-3" data-toggle="tab" href="#tab-3" role="tab" aria-controls="tab-3" aria-selected="false"><i class="fas fa-magic"></i>Автоматизация</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="templateTabsContent">
                        <div class="tab-pane fade show active" id="tab-1" role="tabpanel" aria-labelledby="tab-1">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="image-container">
                                            <img class="img-fluid rounded" src="../../Content/img/Main.png" alt="alternative">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="text-container">
                                            <h3>Всё что нужно для описания тестов</h3>
                                            <p>Test tracker обладает всеми функциями, необходимыми для быстрого и удобного создания тест плана для вашего проекта. Добавление новых тестов и этапов тестирования или изменение старых, поиск, сортировка и фильтрация тестов. Test tracker станет не заменим инструментом для любого тестировщка.</p>
                                            <ul class="list-unstyled li-space-lg">
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Создание тест плана для вашего проекта</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Поиск, фильтрация и сортировка ваших тестов по датам, статусу и не только</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Современный и удобный интерфейс</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Работайте с любого устройства</div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab-2" role="tabpanel" aria-labelledby="tab-2">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="image-container">
                                            <img class="img-fluid rounded" src="../../Content/img/Excel.png" alt="alternative">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="text-container">
                                            <h3>Создание отчёта проще, чем когда-либо</h3>
                                            <p>Забудьте о работе в старых электронных таблицах. Теперь для создания отчёта по тестированию достаточно нажать одну кнопку. Приложение создаст полностью готовый к работе Excel отчёт по проекту.</p>
                                            <ul class="list-unstyled li-space-lg">
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Создание отчёта в один клик</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Возможность отфильтровать тесты по датам</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Отчёт, который понравится всем!</div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab-3" role="tabpanel" aria-labelledby="tab-3">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="image-container">
                                            <img class="img-fluid" src="../../Content/img/mail.png" alt="alternative">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="text-container">
                                            <h3>Забудьте о рутине</h3>
                                            <p>Перестаньте думать о мелких и постоянно повторяющихся задачах – Test tracker решит их за вас.</p>
                                            <ul class="list-unstyled li-space-lg">
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Автоматическая изменение версии проекта</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Отправка отчёта прямиком из приложения</div>
                                                </li>
                                                <li class="media">
                                                    <i class="fas fa-square"></i>
                                                    <div class="media-body">Автоматической формирование отчёта</div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="details" class="basic-1">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="text-container">
                            <h2>Современная панель администратора</h2>
                            <p>
                               Панель администратора – одна из важнейших частей любого программного продукта, работающего с большим объёмом данных. Она предназначена для управления данными, недоступными для обычного пользователя. Администратор сможет создавать новые проекты, управлять данными о пользователях и назначать участников проектов.<p>
                                    <ul class="list-unstyled li-space-lg">
                                        <li class="media">
                                            <i class="fas fa-square"></i>
                                            <div class="media-body">Удобный и современны интерфейс</div>
                                        </li>
                                        <li class="media">
                                            <i class="fas fa-square"></i>
                                            <div class="media-body">Простота в работе</div>
                                        </li>
                                        <li class="media">
                                            <i class="fas fa-square"></i>
                                            <div class="media-body">Обладает всеми необходимыми функциями для администратора</div>
                                        </li>
                                    </ul>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="image-container ">
                            <img class="img-fluid rounded" src="../../Content/img/Admin.png" alt="alternative" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="basic-3">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="icon-container">
                            <span class="fa-stack">
                                <a href="https://github.com/MaksimGMD/TestTracker">
                                    <i class="fas fa-circle fa-stack-2x"></i>
                                    <i class="fab fa-github fa-stack-1x"></i>
                                </a>
                            </span>
                            <span class="fa-stack">
                                <a href="https://www.lanit.ru/">
                                    <i class="fas fa-circle fa-stack-2x"></i>
                                    <i class="fas fa-globe fa-stack-1x"></i>
                                </a>
                            </span>
                            <span class="fa-stack">
                                <a href="https://vk.com/47hramasom">
                                    <i class="fas fa-circle fa-stack-2x"></i>
                                    <i class="fab fa-vk fa-stack-1x"></i>
                                </a>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer-decoration">
            <svg class="footer-frame" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="none" viewBox="0 0 1920 115.984">
                <defs>
                    <style>
                        .cls-2 {
                            fill: #13b987;
                        }
                    </style>
                </defs><title>footer-frame</title><path class="cls-2" d="M1616.28,381.705c-87.656,5.552-93.356-59.62-197.369-67.562-112.391-8.581-137.609,65.077-251.189,60.632-109.57-4.288-116.156-74.017-225.25-83.153-125.171-10.483-150,78.5-293,88.35-136.948,9.437-173.108-68.092-320.83-77.956C255.5,297.133,143,308.1,0,395.564V406.75H1920V324.537c-18-6.507-43.63-14.492-76.1-15.591C1740.655,305.452,1705.829,376.033,1616.28,381.705Z" transform="translate(0 -290.766)" /></svg>
        </div>
        <div class="copyright">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <p class="p-small">Copyright © ЛАНИТ</p>
                    </div>
                </div>
            </div>
        </div>


        <!-- Scripts -->
        <script src="js/jquery.min.js"></script>
        <!-- jQuery for Bootstrap's JavaScript plugins -->
        <script src="js/bootstrap.min.js"></script>
        <!-- Bootstrap framework -->
        <script src="js/jquery.easing.min.js"></script>
        <!-- jQuery Easing for smooth scrolling between anchors -->
        <script src="js/swiper.min.js"></script>
        <!-- Swiper for image and text sliders -->
        <script src="js/jquery.magnific-popup.js"></script>
        <!-- Magnific Popup for lightboxes -->
        <script src="js/scripts.js"></script>
        <!-- Custom scripts -->
    </form>
</body>
</html>
