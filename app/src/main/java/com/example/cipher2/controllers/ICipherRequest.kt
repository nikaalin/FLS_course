package com.example.cipher2.controllers

import com.example.cipher2.ProjectValues.ip
import com.example.cipher2.models.TextModel

abstract class ICipherRequest {
    abstract val response: TextModel
    abstract val status: Int

    fun decryptUrl(key: String, format: String): String =
        "http://$ip/VigenereCipherMew/api/Decrypt?key=$key&format=$format"

    fun encryptUrl(key: String, format: String): String =
        "http://$ip/VigenereCipherMew/api/Encrypt?key=$key&format=$format"

    abstract suspend fun sendEncrypt()
    abstract suspend fun sendDecrypt()
}