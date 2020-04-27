package com.example.cipher2.controllers

import com.example.cipher2.models.TextModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.gson.responseObject
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.withContext

class StringCipherRequest(
    private val key: String,
    private val doc: String?
) : ICipherRequest {
    lateinit var response: TextModel
    override fun getTextModelResponse(): TextModel {
        return response
    }

    override suspend fun sendEncrypt() {
        post(encryptUrl(key, "string"))
    }

    override suspend fun sendDecrypt() {
        post(decryptUrl(key, "string"))
    }

    private suspend fun post(url: String) {
        response = withContext(IO) {
            return@withContext Fuel.post(url)
                .set("Content-Type", "text/plain")
                .timeout(Int.MAX_VALUE).timeoutRead(Int.MAX_VALUE)
                .body(doc!!).responseObject<TextModel>().third.get()
        }
    }
}