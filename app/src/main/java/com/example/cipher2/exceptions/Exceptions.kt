package com.example.cipher2.exceptions

import android.app.Activity
import org.jetbrains.anko.toast

abstract class ApplicationException : Exception() {
    abstract fun toast(activity: Activity)
}

class FileAlreadyExists: ApplicationException() {
    override fun toast(activity: Activity) {
        activity.toast("Файл с таким именем уже существует")
    }
}

class NoKeyException: ApplicationException() {
    override fun toast(activity: Activity) {
        activity.toast("Нужен ключ!")
    }
}
class NoFileUploadException: ApplicationException() {
    override fun toast(activity: Activity) {
        activity.toast("Выберите файл!")
    }
}

class BadResponseException: ApplicationException() {
    override fun toast(activity: Activity) {
        activity.toast("Ошибка сервера")
    }
}


