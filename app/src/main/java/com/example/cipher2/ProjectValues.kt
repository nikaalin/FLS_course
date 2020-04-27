package com.example.cipher2

import android.net.Uri

object ProjectValues {
    const val ip = "192.168.0.101"

    const val defaultKey = "скорпион"
    const val FILE_PICKER_REQUEST_CODE = 42
    const val DIRECTORY_PICKER_REQUEST_CODE = 34
    const val FILE_NAME_REQUEST_CODE = 51

    var fileUriToUpload: Uri? = null
    var fileNameToSave: String? = null
    var fileTypeToSave: String? = "txt"
    var directoryUriToSave: Uri? = null
}